namespace EPMS.Application.DTOs.TemplateDTOs
{
    public class CreateCriteriaRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public Guid SectionId { get; set; }
    }
}
