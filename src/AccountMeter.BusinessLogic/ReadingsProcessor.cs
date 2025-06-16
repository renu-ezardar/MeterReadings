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

                IList<Reading> invalidReadings = DateFormatAndGetInvalidReadings(readings);
                IList<Reading> orphanReadings = GetOrphanReadings(readings.Except(invalidReadings).ToList());
                IList<Reading> validReadings = readings.Except(invalidReadings).Except(orphanReadings).ToList();

                if(validReadings.Any())
                {
                    //As All validations are done already so adding all readings together to save DB connections, 
                    //but if data is more comlicated, I will prefer saving data one by one or in batches 
                    var result = _repository.AddMeterReadings(validReadings.Select(x=> x.ToEntity()).ToList());
                    if (result)
                        return new ReadingProcessResponse()
                        {
                            FailureReadingCount = orphanReadings.Count + invalidReadings.Count,
                            SuccessfulReadingCount = validReadings.Count
                        };
                }
                return new ReadingProcessResponse() { FailureReadingCount = 0, SuccessfulReadingCount = 0 };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in proessing meter readings {JsonSerializer.Serialize(ex)}");
                return new ReadingProcessResponse() { Success = false, ErrorMessage = ex.Message };
            }

        }

        private IList<Reading> GetOrphanReadings(List<Reading> readings)
        {
            var allAccountIds = _repository.GetAllAccountIds();
            return readings.Where(mr => !allAccountIds.Select(a=> a).Contains(mr.AccountId)).ToList();
        }

        private IList<Reading> DateFormatAndGetInvalidReadings(IList<Reading> readings)
        {
           var invalidReadings = new List<Reading>();
            var seenkey = new HashSet<string>();
            string[] inputDateFormats = new string[] { "dd/MM/yyyy HH:mm", "dd/MM/yyyy hh:mm:ss tt" };
           foreach(Reading reading in readings)
            {
                if (reading.AccountId <= 0
                    || reading.MeterReadValue <= 0
                    || !DateTime.TryParseExact(reading.MeterReadingDateTime, inputDateFormats,
                    null, System.Globalization.DateTimeStyles.None, out DateTime result))
                {
                    invalidReadings.Add(reading);
                    continue;
                }
                else
                    reading.MeterReadingDateTimeFormatted = result;//converted in correct date format to save value in db

                //Check if this is duplicate for same day
                string key = $"{reading.AccountId}_{result.Date:yyyyMMdd}";
                if (!seenkey.Add(key))
                invalidReadings.Add(reading);

            }
            return invalidReadings;
        }
    }
}
