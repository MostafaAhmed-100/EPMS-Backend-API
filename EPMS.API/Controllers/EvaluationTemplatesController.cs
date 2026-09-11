using EPMS.Application.Constants;
using EPMS.Application.DTOs.Shared;
using EPMS.Application.DTOs.TemplateDTOs;
using EPMS.Application.Services.EvaluationTemplateService.CommandService;
using EPMS.Application.Services.EvaluationTemplateService.QueryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EvaluationTemplatesController : ControllerBase
    {
        private readonly IEvaluationTemplateCommandService _commandService;
        private readonly IEvaluationTemplateQueryService _queryService;

        public EvaluationTemplatesController(
            IEvaluationTemplateCommandService commandService,
            IEvaluationTemplateQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpPost]
        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR}")]
        [ProducesResponseType(typeof(ApiResponseDto<Guid>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateTemplateRequest request)
        {
            var result = await _commandService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPatch("{id:guid}/deactivate")]
        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR}")]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)
        {
            var result = await _commandService.DeactivateAsync(id);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR},{AppRoles.Manager}")]
        [ProducesResponseType(typeof(ApiResponseDto<EvaluationTemplateResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _queryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("active")]
        [ProducesResponseType(typeof(ApiResponseDto<IReadOnlyList<EvaluationTemplateResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveTemplates()
        {
            var result = await _queryService.GetActiveTemplatesAsync();
            return Ok(result);
        }
    }
}
