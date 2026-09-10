namespace EPMS.Application.DTOs.TemplateDTOs
{
    public class CriteriaResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Weight { get; set; }
    }
}
