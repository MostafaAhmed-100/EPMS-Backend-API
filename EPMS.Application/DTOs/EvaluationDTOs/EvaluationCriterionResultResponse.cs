namespace EPMS.Application.DTOs.EvaluationDTOs
{
    public class EvaluationCriterionResultResponse
    {
        public Guid CriteriaId { get; set; }
        public string CriteriaName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }
}
