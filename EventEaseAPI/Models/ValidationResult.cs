namespace EventEaseAPI.Models
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class OperationResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static OperationResult Success() => new OperationResult { Success = true };
        public static OperationResult Failure(params string[] errors) =>
            new OperationResult { Success = false, Errors = errors.ToList() };
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }
}
