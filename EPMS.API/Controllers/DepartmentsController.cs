using EPMS.Application.Constants;
using EPMS.Application.DTOs.DepartmentDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Application.Services.DepartmentQueryService.CommandService;
using EPMS.Application.Services.DepartmentQueryService.QueryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentCommandService _commandService;
        private readonly IDepartmentQueryService _queryService;

        public DepartmentsController(
            IDepartmentCommandService commandService,
            IDepartmentQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.Admin)]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentRequest request)
        {
            var result = await _commandService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = AppRoles.Admin)]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDepartmentRequest request)
        {
            var result = await _commandService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _queryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<IReadOnlyList<DepartmentResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _queryService.GetAllAsync();
            return Ok(result);
        }
    }
}
