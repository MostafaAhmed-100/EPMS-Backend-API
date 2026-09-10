using EPMS.Domain.Enums;

namespace EPMS.Application.DTOs.EvaluationDTOs
{
    public class EvaluationSummaryResponse
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public Guid EvaluatorId { get; set; }
        public string EvaluatorName { get; set; } = string.Empty;
        public string TemplateTitle { get; set; } = string.Empty;
        public EvaluationStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
