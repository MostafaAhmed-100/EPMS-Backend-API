namespace EPMS.Domain.ValueObjects
{
    public record DateRange
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public DateRange (DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("The Start Date cant be after the End Date");
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
