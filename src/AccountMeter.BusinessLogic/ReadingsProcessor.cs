using AccountMeter.Common.Models;
using AccountMeter.Infrastructure;
using System.Text.Json;

namespace AccountMeter.BusinessLogic
{
    public class ReadingsProcessor : IReadingsProcessor
    {
        private readonly IRepository _repository;
        public ReadingsProcessor(IRepository repository)
        {
            _repository = repository;
            
        }
        public ReadingProcessResponse ProcessReadings(IList<Reading> readings)
        {
            try {

                return new ReadingProcessResponse() { FailureReadingCount = 0, SuccessfulReadingCount = 0 };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in proessing meter readings {JsonSerializer.Serialize(ex)}");
                return new ReadingProcessResponse() { Success = false, ErrorMessage = ex.Message };
            }

        }
    }
}
