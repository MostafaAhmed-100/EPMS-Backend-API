using System;
using System.Collections.Generic;
using System.Text;

namespace EPMS.Domain.Entitys
{
    public class Criteria : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Weight { get; private set; }  //الاهميه بتعتو في التقييم
        public Guid SectionId { get; private set; }

        public Section Section { get; private set; }

        public Criteria(string name, string description, decimal weight, Guid sectionId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Criteria name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            if (weight <= 0 || weight > 100)
                throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be between 0 and 100.");

            if (sectionId == Guid.Empty)
                throw new ArgumentException("Valid section ID is required.", nameof(sectionId));

            Name = name;
            Description = description;
            Weight = weight;
            SectionId = sectionId;
        }

        public void UpdateDetails(string name, string description, decimal weight)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Criteria name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            if (weight <= 0 || weight > 100)
                throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be between 0 and 100.");

            Name = name;
            Description = description;
            Weight = weight;
        }
    }
}
