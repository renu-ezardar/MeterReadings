using AccountMeter.Common.Models;

namespace AccountMeter.BusinessLogic
{
    public interface IReadingsProcessor
    {
        ReadingProcessResponse ProcessReadings(IList<Reading> readings);
    }
}
