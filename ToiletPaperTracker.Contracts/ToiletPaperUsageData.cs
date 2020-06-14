
using System;
using System.Collections.Generic;

namespace ToiletPaperTracker.Contracts
{
    public class ToiletPaperUsageData
    {
        public int NumberOfToiletPaperRollsRemaining { get; set;}
        
        public IEnumerable<DateTime> DataPoints { get; set;}
    }
}