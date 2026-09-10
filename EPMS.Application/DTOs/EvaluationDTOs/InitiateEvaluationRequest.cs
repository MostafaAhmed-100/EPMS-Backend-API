namespace EPMS.Application.DTOs.EvaluationDTOs
{
    public class InitiateEvaluationRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid EvaluatorId { get; set; }
        public Guid TemplateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
