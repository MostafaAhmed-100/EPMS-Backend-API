using EPMS.Application.Constants;
using EPMS.Application.DTOs.AuthDTOs;
using EPMS.Application.DTOs.EmployeeDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Application.Services.EmployeeService.CommandService;
using EPMS.Domain.Interfaces;
using EPMS.Application.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EPMS.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmployeeCommandService _employeeCommandService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<IdentityUser<Guid>> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IEmployeeCommandService employeeCommandService,
            IUnitOfWork unitOfWork,
            IOptions<JwtSettings> jwtOptions,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeCommandService = employeeCommandService;
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtOptions.Value;
            _logger = logger;
        }

        public async Task<ApiResponseDto<AuthResponseDto>> RegisterEmployeeAsync(RegisterEmployeeRequestDto request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed: Email {Email} is already registered.", request.Email);
                throw new InvalidOperationException("This email already has an account.");
            }

            var assignedRole = string.IsNullOrWhiteSpace(request.Role) ? AppRoles.Employee : request.Role;
            if (!await _roleManager.RoleExistsAsync(assignedRole))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = assignedRole });
            }

            var newId = Guid.NewGuid();

            var identityUser = new IdentityUser<Guid>
            {
                Id = newId,
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var createResult = await _userManager.CreateAsync(identityUser, request.Password);
                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Account creation failed: {errors}");
                }

                var roleResult = await _userManager.AddToRoleAsync(identityUser, assignedRole);
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to assign role: {errors}");
                }

                var employeeCreateCommand = new CreateEmployeeRequest
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    JobTitle = request.JobTitle,
                    HireDate = request.HireDate,
                    DepartmentId = request.DepartmentId
                };

                var employeeResult = await _employeeCommandService.CreateAsync(employeeCreateCommand);
                var createdEmployee = employeeResult.Data;

                await transaction.CommitAsync();

                _logger.LogInformation("Employee {EmployeeId} registered successfully with role {Role}.", createdEmployee.Id, assignedRole);

                var token = GenerateJwtToken(identityUser, assignedRole, createdEmployee.Id);

                return new ApiResponseDto<AuthResponseDto>
                {
                    Message = "Employee account created and registered successfully.",
                    Data = new AuthResponseDto
                    {
                        Token = token,
                        Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                        Email = identityUser.Email,
                        Role = assignedRole,
                        EmployeeId = createdEmployee.Id
                    }
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred during RegisterEmployeeAsync for {Email}", request.Email);
                throw;
            }
        }

        public async Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogWarning("Failed login attempt for email: {Email}", request.Email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var primaryRole = roles.FirstOrDefault() ?? AppRoles.Employee;

            var employeeId = user.Id;

            var token = GenerateJwtToken(user, primaryRole, employeeId);

            _logger.LogInformation("User {UserId} logged in successfully.", user.Id);

            return new ApiResponseDto<AuthResponseDto>
            {
                Message = "Login successful.",
                Data = new AuthResponseDto
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    Email = user.Email!,
                    Role = primaryRole,
                    EmployeeId = employeeId
                }
            };
        }

        private string GenerateJwtToken(IdentityUser<Guid> user, string role, Guid employeeId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, role),
                new Claim("EmployeeId", employeeId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
