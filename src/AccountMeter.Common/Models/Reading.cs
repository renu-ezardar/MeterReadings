using CsvHelper.Configuration;

namespace AccountMeter.Common.Models
{
    public class Reading
    {
        public int AccountId { get; set; }
        public string MeterReadingDateTime { get; set; } = string.Empty;
        public DateTime MeterReadingDateTimeFormatted { get; set; }
    public int MeterReadValue { get; set; }

    }

    public class ReadingMap : ClassMap<Reading>
    {
        public ReadingMap()
        {
            Map(m => m.AccountId);
            Map(m => m.MeterReadingDateTime);
            //ignore MeterReadingDateTimeFormatted
            Map(m => m.MeterReadValue);

        }
    }
}
