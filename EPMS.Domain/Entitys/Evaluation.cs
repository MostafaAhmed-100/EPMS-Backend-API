using EPMS.Domain.Enums;
using EPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPMS.Domain.Entitys
{
    public class Evaluation : BaseEntity
    {
        public Guid EmployeeId { get; private set; }
        public Guid EvaluatorId { get; private set; }
        public Guid TemplateId { get; private set; }
        public DateRange Period { get; private set; }
        public EvaluationStatus Status { get; private set; }
        public decimal FinalScore { get; private set; }
        public Employee Employee { get; private set; }
        public Employee Evaluator { get; private set; }
        public EvaluationTemplate Template { get; private set; }

        private readonly List<EvaluationResponse> _responses = new();
        public IReadOnlyCollection<EvaluationResponse> Responses => _responses.AsReadOnly();
        protected Evaluation() { }
        public void CalculateFinalScore(EvaluationTemplate template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (_responses.Count == 0)
            {
                FinalScore = 0;
                return;
            }

            decimal totalScore = 0;

            foreach (var section in template.Sections)
            {
                decimal sectionScore = 0;

                foreach (var criteria in section.Criteria)
                {
                    var response = _responses.FirstOrDefault(r => r.CriteriaId == criteria.Id);
                    if (response != null)
                    {
                        sectionScore += response.Score * (criteria.Weight / 100m);
                    }
                }

                totalScore += sectionScore * (section.Weight / 100m);
            }

            FinalScore = Math.Round(totalScore, 2);
        }

        public Evaluation(Guid employeeId, Guid evaluatorId, Guid templateId, DateRange period)
        {
            if (employeeId == Guid.Empty)
                throw new ArgumentException("Valid employee ID is required.", nameof(employeeId));

            if (evaluatorId == Guid.Empty)
                throw new ArgumentException("Valid evaluator ID is required.", nameof(evaluatorId));

            if (employeeId == evaluatorId)
                throw new InvalidOperationException("An employee cannot evaluate themselves.");

            if (templateId == Guid.Empty)
                throw new ArgumentException("Valid template ID is required.", nameof(templateId));

            Period = period ?? throw new ArgumentNullException(nameof(period));

            EmployeeId = employeeId;
            EvaluatorId = evaluatorId;
            TemplateId = templateId;
            Status = EvaluationStatus.Pending;
        }

        public void StartEvaluation()
        {
            if (Status != EvaluationStatus.Pending)
                throw new InvalidOperationException("Evaluation can only be started from pending status.");

            Status = EvaluationStatus.InProgress;
        }

        public void CompleteEvaluation()
        {
            if (Status != EvaluationStatus.InProgress)
                throw new InvalidOperationException("Only an in-progress evaluation can be completed.");

            if (_responses.Count == 0)
                throw new InvalidOperationException("Cannot complete an evaluation with no responses.");

            Status = EvaluationStatus.Completed;
        }

        public void CancelEvaluation()
        {
            if (Status == EvaluationStatus.Completed)
                throw new InvalidOperationException("Completed evaluations cannot be cancelled.");

            Status = EvaluationStatus.Cancelled;
        }

        public void AddResponse(EvaluationResponse response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (Status == EvaluationStatus.Completed || Status == EvaluationStatus.Cancelled)
                throw new InvalidOperationException("Cannot add responses to a finalized evaluation.");

            _responses.Add(response);
        }
    }
}
