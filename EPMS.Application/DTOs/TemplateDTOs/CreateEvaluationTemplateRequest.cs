namespace EPMS.Application.DTOs.TemplateDTOs
{
    public class CreateEvaluationTemplateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<CreateSectionRequest> Sections { get; set; } = new();
    }
}
