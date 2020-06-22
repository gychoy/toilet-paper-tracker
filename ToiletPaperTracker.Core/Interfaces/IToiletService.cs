using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToiletPaperTracker.Core.Interfaces
{
    public interface IToiletService 
    {
        Task<int> GetDaysRemaining();

        Task<int> GetNumberOfRollsRemaining();

        Task AddUsageData(DateTime date);

        Task RemoveUsageData(DateTime date);

        Task<IEnumerable<DateTime>> GetDataPoints();

        Task<DateTime> GetDateWhenToiletPaperRunsOut();

        Task UpdateNumberOfRollsRemaining(int number);
    }
}