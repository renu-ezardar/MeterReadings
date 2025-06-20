﻿using AccountMeter.API.Entities;

namespace AccountMeter.Infrastructure
{
    public class Repository : IRepository
    {
        AccountMeterTestContext _dbcontext;
        public Repository(AccountMeterTestContext context)
        {

            _dbcontext = context;
        }
        public bool AddMeterReadings(IList<MeterReading> readings)
        {
            try
            {
                _dbcontext.MeterReadings.AddRange(readings);
                _dbcontext.SaveChanges();
                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        public IList<int> GetAllAccountIds()
        {
            try
            {
                return _dbcontext.Accounts.Select(a=> a.AccountId).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
