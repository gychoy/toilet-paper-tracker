
using System;
using System.Collections.Generic;

namespace toilet_paper_tracker_blazorserver.Data
{
    public class ToiletPaperUsageData
    {
        public int NumberOfToiletPaperRollsRemaining { get; set;}
        
        public IEnumerable<DateTime> DataPoints { get; set;}
    }
}