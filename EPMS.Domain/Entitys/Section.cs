namespace EPMS.Domain.Entitys
{
    public class Section : BaseEntity
    { 
        public string Name { get; private set; }
        public decimal Weight { get; private set; } //الاهميه بتعتو في التقييم
        public Guid TemplateId { get; private set; }

        public EvaluationTemplate Template { get; private set; }

        private readonly List<Criteria> _criteria = new();
        public IReadOnlyCollection<Criteria> Criteria => _criteria.AsReadOnly();

        public Section(string name, decimal weight, Guid templateId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Section name is required.", nameof(name));

            if (weight <= 0 || weight > 100)
                throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be between 0 and 100.");

            if (templateId == Guid.Empty)
                throw new ArgumentException("Valid template ID is required.", nameof(templateId));

            Name = name;
            Weight = weight;
            TemplateId = templateId;
        }

        public void UpdateDetails(string name, decimal weight)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Section name is required.", nameof(name));

            if (weight <= 0 || weight > 100)
                throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be between 0 and 100.");

            Name = name;
            Weight = weight;
        }

        public void AddCriteria(Criteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            _criteria.Add(criteria);
        }
    }
}
