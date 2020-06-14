using System;
using System.Collections.Generic;
using System.Linq;
using ToiletPaperTracker.Core.Interfaces;

namespace ToiletPaperTracker.Core
{
    public class ToiletService : IToiletService
    {
        private readonly IToiletRepository _repository;

        public ToiletService(IToiletRepository repository)
        {
            _repository = repository;
        }

        public void AddUsageData(DateTime date) => _repository.AddUsageData(date);

        public int GetNumberOfRollsRemaining() => _repository.GetNumberOfRollsRemaining();        

        public IEnumerable<DateTime> GetDataPoints() => _repository.GetUsageData().OrderBy(d => d);

        public int GetDaysRemaining()
        {
            var rollsRemaining = _repository.GetNumberOfRollsRemaining();
            var datesConsumed = _repository.GetUsageData().OrderBy(x => x).ToList();

            var toiletPaperDays = new List<int>();
            for(var i = 1; i < datesConsumed.Count(); i++)
            {
                toiletPaperDays.Add((datesConsumed[i] - datesConsumed[i - 1]).Days);
            }

            return rollsRemaining * (int)Math.Floor((double)toiletPaperDays.Sum() / toiletPaperDays.Count());
        }

        public DateTime GetDateWhenToiletPaperRunsOut() => DateTime.Now.Date.AddDays(GetDaysRemaining());

        public void UpdateNumberOfRollsRemaining(int number) => _repository.UpdateNumberOfRollsRemaining(number);

        public void RemoveUsageData(DateTime date) => _repository.RemoveUsageData(date);
    }
}