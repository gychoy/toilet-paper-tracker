
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using toilet_paper_tracker_blazorserver.Interfaces;

namespace toilet_paper_tracker_blazorserver.Data
{
    public class ToiletRepository : IToiletRepository
    {
        private const string DATA_FILE_NAME = @"Data\data.json";

        public void AddUsageData(DateTime date)
        {
            var existinData = ReadJsonFile();

            var dataPoints = existinData.DataPoints.ToList();
            dataPoints.Add(date);

            existinData.DataPoints = dataPoints;

            WriteJsonFile(existinData);
        }

        public int GetNumberOfRollsRemaining() =>ReadJsonFile().NumberOfToiletPaperRollsRemaining;        

        public IEnumerable<DateTime> GetUsageData() => ReadJsonFile().DataPoints;        

        public void RemoveUsageData(DateTime date)
        {
            var existinData = ReadJsonFile();

            var dataPoints = existinData.DataPoints.ToList();
            dataPoints.Remove(date);

            existinData.DataPoints = dataPoints;

            WriteJsonFile(existinData);
        }

        private ToiletPaperUsageData ReadJsonFile()
        {
            var fileData = File.ReadAllText(DATA_FILE_NAME);
            return JsonConvert.DeserializeObject<ToiletPaperUsageData>(fileData);
        }

        private void WriteJsonFile(ToiletPaperUsageData data)
        {
            var serializedData = JsonConvert.SerializeObject(data);

            File.WriteAllText(DATA_FILE_NAME, serializedData);
        }
    }
}