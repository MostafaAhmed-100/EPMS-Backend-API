namespace EPMS.Application.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public Guid EmployeeId { get; set; }
    }
}
