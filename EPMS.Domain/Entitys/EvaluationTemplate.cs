namespace EPMS.Domain.Entitys
{
    public class EvaluationTemplate : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }

        private readonly List<Section> _sections = new();
        public IReadOnlyCollection<Section> Sections => _sections.AsReadOnly();
        public EvaluationTemplate(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            Title = title;
            Description = description;
            IsActive = true;
        }
        public void UpdateDetails(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            Title = title;
            Description = description;
        }
        public void AddSection(Section section)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));

            _sections.Add(section);
        }
        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Template is already inactive.");

            IsActive = false;
        }
        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Template is already active.");

            IsActive = true;
        }
    }
}