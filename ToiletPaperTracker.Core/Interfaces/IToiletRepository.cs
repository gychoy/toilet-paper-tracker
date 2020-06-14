
using System;
using System.Collections.Generic;

namespace ToiletPaperTracker.Core.Interfaces
{
    public interface IToiletRepository
    {
        IEnumerable<DateTime> GetUsageData();

        int GetNumberOfRollsRemaining();

        void UpdateNumberOfRollsRemaining(int number);

        void AddUsageData(DateTime date);

        void RemoveUsageData(DateTime date);
    }
}