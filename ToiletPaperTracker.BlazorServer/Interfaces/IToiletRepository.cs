
using System;
using System.Collections.Generic;

namespace toilet_paper_tracker_blazorserver.Interfaces
{
    public interface IToiletRepository
    {
        IEnumerable<DateTime> GetUsageData();

        int GetNumberOfRollsRemaining();

        void AddUsageData(DateTime date);

        void RemoveUsageData(DateTime date);
    }
}