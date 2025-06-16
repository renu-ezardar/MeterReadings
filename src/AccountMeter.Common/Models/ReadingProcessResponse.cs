namespace AccountMeter.Common.Models
{
    public class ReadingProcessResponse
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
        public int SuccessfulReadingCount { get; set; }
        public int FailureReadingCount { get; set; }
    }
}