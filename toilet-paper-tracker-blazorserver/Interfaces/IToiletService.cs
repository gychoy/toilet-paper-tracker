using System;
using System.Collections.Generic;

namespace toilet_paper_tracker_blazorserver.Interfaces
{
    public interface IToiletService 
    {
        int GetDaysRemaining();

        int GetNumberOfRollsRemaining();

        void AddUsageData(DateTime date);

        IEnumerable<DateTime> GetDataPoints();

        DateTime GetDateWhenToiletPaperRunsOut();
    }
}