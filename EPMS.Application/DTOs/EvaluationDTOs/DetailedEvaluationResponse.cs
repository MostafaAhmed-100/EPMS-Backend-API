using EPMS.Domain.Enums;

namespace EPMS.Application.DTOs.EvaluationDTOs
{
    public class DetailedEvaluationResponse
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EvaluatorName { get; set; } = string.Empty;
        public string TemplateTitle { get; set; } = string.Empty;
        public EvaluationStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal OverallScore { get; set; }
        public List<EvaluationCriterionResultResponse> CriteriaResults { get; set; } = new();
    }
}
