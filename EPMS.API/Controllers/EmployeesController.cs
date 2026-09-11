using EPMS.Application.Constants;
using EPMS.Application.DTOs.EmployeeDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Application.Services.EmployeeService.CommandService;
using EPMS.Application.Services.EmployeeService.QueryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeCommandService _commandService;
        private readonly IEmployeeQueryService _queryService;

        public EmployeesController(
            IEmployeeCommandService commandService,
            IEmployeeQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpPost]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR}")]
        [ProducesResponseType(typeof(ApiResponseDto<EmployeeResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
        {
            var result = await _commandService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id:guid}/department")]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR}")]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeDepartment([FromRoute] Guid id, [FromBody] UpdateEmployeeDepartmentRequest request)
        {
            var result = await _commandService.ChangeDepartmentAsync(id, request);
            return Ok(result);
        }

        [HttpPatch("{id:guid}/deactivate")]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR}")]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)
        {
            var result = await _commandService.DeactivateAsync(id);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR},{AppRoles.Manager}")]
        [ProducesResponseType(typeof(ApiResponseDto<EmployeeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _queryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR},{AppRoles.Manager}")]
        [ProducesResponseType(typeof(ApiResponseDto<IReadOnlyList<EmployeeResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _queryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("department/{departmentId:guid}")]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR},{AppRoles.Manager}")]
        [ProducesResponseType(typeof(ApiResponseDto<IReadOnlyList<EmployeeResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByDepartmentId([FromRoute] Guid departmentId)
        {
            var result = await _queryService.GetByDepartmentIdAsync(departmentId);
            return Ok(result);
        }
    }
}
