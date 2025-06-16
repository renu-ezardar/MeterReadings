namespace AccountMeter.API.Models
{
    public class FileUploadResponse
    {
       
        public int SuccessfulReadingCount { get; set; }
        public int FailureReadingCount { get; set; }
    }
}