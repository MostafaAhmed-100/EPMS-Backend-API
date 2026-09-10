namespace EPMS.Application.DTOs.TemplateDTOs
{
    public class CriterionResponseRequest
    {
        public Guid CriteriaId { get; set; }
        public decimal Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }
}
