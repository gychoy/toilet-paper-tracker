using System;
using System.Collections.Generic;

namespace ToiletPaperTracker.Core.Interfaces
{
    public interface IToiletService 
    {
        int GetDaysRemaining();

        int GetNumberOfRollsRemaining();

        void AddUsageData(DateTime date);

        void RemoveUsageData(DateTime date);

        IEnumerable<DateTime> GetDataPoints();

        DateTime GetDateWhenToiletPaperRunsOut();

        void UpdateNumberOfRollsRemaining(int number);
    }
}