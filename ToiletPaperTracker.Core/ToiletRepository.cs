
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using ToiletPaperTracker.Contracts;
using ToiletPaperTracker.Core.Interfaces;

namespace ToiletPaperTracker.Core
{
    public class ToiletRepository : IToiletRepository
    {
        private const string DATA_FILE_NAME = "\\Data\\data.json";

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

        public void UpdateNumberOfRollsRemaining(int number)
        {
            var existinData = ReadJsonFile();
            existinData.NumberOfToiletPaperRollsRemaining = number;

            WriteJsonFile(existinData);
        }

        private ToiletPaperUsageData ReadJsonFile()
        {
            var fileData = File.ReadAllText($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}{DATA_FILE_NAME}");
            return JsonConvert.DeserializeObject<ToiletPaperUsageData>(fileData);
        }

        private void WriteJsonFile(ToiletPaperUsageData data)
        {
            var serializedData = JsonConvert.SerializeObject(data);

            File.WriteAllText($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}{DATA_FILE_NAME}", serializedData);
        }
    }
}