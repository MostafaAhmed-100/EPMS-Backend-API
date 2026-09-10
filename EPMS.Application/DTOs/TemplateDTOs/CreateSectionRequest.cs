namespace EPMS.Application.DTOs.TemplateDTOs
{
    public class CreateSectionRequest
    {
        public Guid TemplateId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public List<CreateCriteriaRequest> Criteria { get; set; } = new();
    }
}
