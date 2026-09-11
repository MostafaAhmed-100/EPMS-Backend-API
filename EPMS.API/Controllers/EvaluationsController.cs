using EPMS.Application.Constants;
using EPMS.Application.DTOs.EvaluationDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Application.Services.EvaluationsService.CommandService;
using EPMS.Application.Services.EvaluationsService.QueryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EvaluationsController : ControllerBase
    {
        private readonly IEvaluationCommandService _commandService;
        private readonly IEvaluationQueryService _queryService;

        public EvaluationsController(
            IEvaluationCommandService commandService,
            IEvaluationQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpPost("initiate")]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR},{AppRoles.Manager}")]
        [ProducesResponseType(typeof(ApiResponseDto<Guid>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Initiate([FromBody] InitiateEvaluationRequest request)
        {
            var result = await _commandService.InitiateEvaluationAsync(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("{id:guid}/submit-scores")]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Manager}")]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SubmitScores([FromRoute] Guid id, [FromBody] SubmitEvaluationResponsesRequest request)
        {
            var result = await _commandService.SubmitScoresAsync(id, request);
            return Ok(result);
        }

        [HttpPatch("{id:guid}/cancel")]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR}")]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Cancel([FromRoute] Guid id)
        {
            var result = await _commandService.CancelEvaluationAsync(id);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<DetailedEvaluationResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDetailedById([FromRoute] Guid id)
        {
            var result = await _queryService.GetDetailedByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("employee/{employeeId:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<IReadOnlyList<EvaluationSummaryResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmployeeId([FromRoute] Guid employeeId)
        {
            var result = await _queryService.GetByEmployeeIdAsync(employeeId);
            return Ok(result);
        }

        [HttpGet("evaluator/{evaluatorId:guid}")]
        //[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.HR},{AppRoles.Manager}")]
        [ProducesResponseType(typeof(ApiResponseDto<IReadOnlyList<EvaluationSummaryResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEvaluatorId([FromRoute] Guid evaluatorId)
        {
            var result = await _queryService.GetByEvaluatorIdAsync(evaluatorId);
            return Ok(result);
        }
    }
}
