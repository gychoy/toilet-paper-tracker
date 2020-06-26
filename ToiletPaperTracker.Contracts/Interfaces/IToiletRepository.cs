
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToiletPaperTracker.Contracts.Interfaces
{
    public interface IToiletRepository
    {
        Task<IEnumerable<DateTime>> GetUsageData();

        Task<int> GetNumberOfRollsRemaining();

        Task UpdateNumberOfRollsRemaining(int number);

        Task AddUsageData(DateTime date);

        Task RemoveUsageData(DateTime date);
    }
}