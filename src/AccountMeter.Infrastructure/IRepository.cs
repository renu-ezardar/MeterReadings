using AccountMeter.API.Entities;

namespace AccountMeter.Infrastructure
{
    public interface IRepository
    {
        IEnumerable<Account> GetAllAccounts();
        bool AddMeterReadings(IList<MeterReading> readings);
    }
}
