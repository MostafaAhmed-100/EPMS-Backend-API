namespace EPMS.Application.DTOs.TemplateDTOs
{
    public class SectionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public List<CriteriaResponse> Criteria { get; set; } = new();
    }
}
