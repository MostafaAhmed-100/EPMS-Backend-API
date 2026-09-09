namespace EPMS.Domain.Entitys
{
    public class EvaluationResponse : BaseEntity
    {
        public Guid EvaluationId { get; private set; }
        public Guid CriteriaId { get; private set; }
        public decimal Score { get; private set; }
        public string Feedback { get; private set; }
        public Evaluation Evaluation { get; private set; }
        public Criteria Criteria { get; private set; }

        public EvaluationResponse(Guid evaluationId, Guid criteriaId, decimal score, string feedback)
        {
            if (evaluationId == Guid.Empty)
                throw new ArgumentException("Valid evaluation ID is required.", nameof(evaluationId));

            if (criteriaId == Guid.Empty)
                throw new ArgumentException("Valid criteria ID is required.", nameof(criteriaId));

            if (score < 1 || score > 5)
                throw new ArgumentOutOfRangeException(nameof(score), "Score must be between 1 and 5.");

            EvaluationId = evaluationId;
            CriteriaId = criteriaId;
            Score = score;
            Feedback = feedback ?? string.Empty;
        }

        public void UpdateResponse(decimal newScore, string newFeedback)
        {
            if (newScore < 1 || newScore > 5)
                throw new ArgumentOutOfRangeException(nameof(newScore), "Score must be between 1 and 5.");

            Score = newScore;
            Feedback = newFeedback ?? string.Empty;
        }
    }
}
