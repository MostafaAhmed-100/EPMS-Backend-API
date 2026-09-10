namespace EPMS.Application.DTOs.TemplateDTOs
{
    public class EvaluationTemplateResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<SectionResponse> Sections { get; set; } = new();
    }
}
