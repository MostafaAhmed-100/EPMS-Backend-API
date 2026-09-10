namespace EPMS.Application.DTOs.EvaluationDTOs
{
    public class SubmitEvaluationResponsesRequest
    {
        public List<CriterionResponseItemRequest> Responses { get; set; } = new();
    }
}
