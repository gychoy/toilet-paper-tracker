using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToiletPaperTracker.Contracts;
using ToiletPaperTracker.Core.Interfaces;

namespace ToiletPaperTracker.Core
{
    public class ToiletRepository : IToiletRepository
    {
        private readonly Blazored.LocalStorage.ILocalStorageService _storage;

        public ToiletRepository(Blazored.LocalStorage.ILocalStorageService storage)
        {
            _storage = storage;
        }

        public async Task AddUsageData(DateTime date)
        {
            var existinData = await GetData();

            var dataPoints = existinData.DataPoints.ToList();
            dataPoints.Add(date);

            existinData.DataPoints = dataPoints;

            await WriteData(existinData);
        }

        public async Task<int> GetNumberOfRollsRemaining() => (await GetData()).NumberOfToiletPaperRollsRemaining;

        public async Task<IEnumerable<DateTime>> GetUsageData() => (await GetData()).DataPoints;

        public async Task RemoveUsageData(DateTime date)
        {
            var existinData = await GetData();

            var dataPoints = existinData.DataPoints.ToList();
            dataPoints.Remove(date);

            existinData.DataPoints = dataPoints;

            await WriteData(existinData);
        }

        public async Task UpdateNumberOfRollsRemaining(int number)
        {
            var existinData = await GetData();
            existinData.NumberOfToiletPaperRollsRemaining = number;

            await WriteData(existinData);
        }

        private async Task<ToiletPaperUsageData> GetData()
        {
            var data = new ToiletPaperUsageData(); ;

            // if data is empty, initialize it and save a new data set
            if (!(await _storage.ContainKeyAsync("data")))
            {
                data = new ToiletPaperUsageData { NumberOfToiletPaperRollsRemaining = 0, DataPoints = new List<DateTime> { DateTime.Now.Date } };
                await WriteData(data);
            }
            else
            {
                data = await _storage.GetItemAsync<ToiletPaperUsageData>("data");
            }

            return data;
        }

        private async Task WriteData(ToiletPaperUsageData data)=> await _storage.SetItemAsync<ToiletPaperUsageData>("data", data);
    }
}