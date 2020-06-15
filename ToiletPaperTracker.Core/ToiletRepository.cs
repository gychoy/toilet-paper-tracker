using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ToiletPaperTracker.Contracts;
using ToiletPaperTracker.Core.Interfaces;

namespace ToiletPaperTracker.Core
{
    public class ToiletRepository : IToiletRepository
    {
        private const string DATA_FILE_NAME = "Data\\data.json";

        //todo: Reading files is not compatible with WASM
        private string TestJsonData = "{\"numberOfToiletPaperRollsRemaining\" : 12, \"dataPoints\":[ \"2020-05-08\", \"2020-05-13\",\"2020-05-14\",\"2020-05-19\",\"2020-05-23\",\"2020-06-01\",\"2020-06-07\",\"2020-06-12\"]}";
        
        public void AddUsageData(DateTime date)
        {
            var existinData = ReadJsonFile();

            var dataPoints = existinData.DataPoints.ToList();
            dataPoints.Add(date);

            existinData.DataPoints = dataPoints;

            WriteJsonFile(existinData);
        }

        public int GetNumberOfRollsRemaining() => ReadJsonFile().NumberOfToiletPaperRollsRemaining;

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
            //todo: Reading files is not compatible with WASM
            //var fileData = File.ReadAllText(DATA_FILE_NAME);

            return JsonConvert.DeserializeObject<ToiletPaperUsageData>(TestJsonData);
        }

        private void WriteJsonFile(ToiletPaperUsageData data)
        {
            var serializedData = JsonConvert.SerializeObject(data);

            TestJsonData = serializedData;

            //todo: Reading files is not compatible with WASM
            //File.WriteAllText(DATA_FILE_NAME, serializedData);
        }
    }
}