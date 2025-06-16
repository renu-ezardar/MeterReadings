using AccountMeter.API.Entities;

namespace AccountMeter.Common.Models
{
    public static class ReadingExtension
    {
        public static MeterReading ToEntity(this Reading model)
        {
            return new MeterReading()
            {
                AccountId = model.AccountId,
                MeterReadingDateTime = model.MeterReadingDateTimeFormatted,
                MeterReadValue = model.MeterReadValue

            };
        }
    }
}
