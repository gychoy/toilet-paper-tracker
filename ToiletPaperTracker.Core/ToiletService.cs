using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddUsageData(DateTime date) => await _repository.AddUsageData(date);

        public async Task<int> GetNumberOfRollsRemaining() => await _repository.GetNumberOfRollsRemaining();        

        public async Task<IEnumerable<DateTime>> GetDataPoints() => (await _repository.GetUsageData()).OrderBy(d => d);

        public async Task<int> GetDaysRemaining()
        {
            var rollsRemaining = await GetNumberOfRollsRemaining();
            var datesConsumed = (await GetDataPoints()).ToList();

            var toiletPaperDays = new List<int>();
            for(var i = 1; i < datesConsumed.Count(); i++)
            {
                toiletPaperDays.Add((datesConsumed[i] - datesConsumed[i - 1]).Days);
            }

            return rollsRemaining * (int)Math.Floor((double)toiletPaperDays.Sum() / toiletPaperDays.Count());
        }

        public async Task<DateTime> GetDateWhenToiletPaperRunsOut()
        {
            var daysRemaining = await GetDaysRemaining();

            if (daysRemaining > 0)
                return DateTime.Now.Date.AddDays(daysRemaining);
            else
                return DateTime.Now.Date;
        }

        public async Task UpdateNumberOfRollsRemaining(int number) => await _repository.UpdateNumberOfRollsRemaining(number);

        public async Task RemoveUsageData(DateTime date) => await _repository.RemoveUsageData(date);
    }
}