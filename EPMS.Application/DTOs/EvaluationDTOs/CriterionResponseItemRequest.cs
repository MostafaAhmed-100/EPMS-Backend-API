namespace EPMS.Application.DTOs.EvaluationDTOs
{
    public class CriterionResponseItemRequest
    {
        public Guid CriteriaId { get; set; }
        public decimal Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }
}
