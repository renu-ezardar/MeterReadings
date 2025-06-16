using AccountMeter.API.Entities;

namespace AccountMeter.Infrastructure
{
    public interface IRepository
    {
        IList<int> GetAllAccountIds();
        bool AddMeterReadings(IList<MeterReading> readings);
    }
}
