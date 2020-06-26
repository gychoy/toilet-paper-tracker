using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToiletPaperTracker.Contracts.Interfaces;
using ToiletPaperTracker.Core;
using ToiletPaperTrackerUnitTests.Framework;

namespace ToiletPaperTrackerUnitTests.Core
{
    [TestFixture]
    internal class ToiletServiceUnitTests
    {
        [Test, AutoMoqData]
        public async Task AddUsageData_GivenValidData_Succeeds(
            [Frozen] Mock<IToiletRepository> mockRepository,
            DateTime data,
            ToiletService sut)
        {
            await sut.AddUsageData(data);

            mockRepository.Verify(x => x.AddUsageData(data), Times.Once);
        }

        [Test, AutoMoqData]
        public async Task GetNumberOfRollsRemaining_GivenValidData_Succeeds(
            [Frozen] Mock<IToiletRepository> mockRepository,
            int number,
            ToiletService sut)
        {
            mockRepository
                .Setup(x => x.GetNumberOfRollsRemaining())
                .Returns(Task.FromResult(number));

            var result = await sut.GetNumberOfRollsRemaining();

            Assert.AreEqual(result, number);
            mockRepository.Verify(x => x.GetNumberOfRollsRemaining(), Times.Once);
        }

        [Test, AutoMoqData]
        public async Task GetDataPoints_GivenValidData_Succeeds(
            [Frozen] Mock<IToiletRepository> mockRepository,
            IEnumerable<DateTime> dataPoints,
            ToiletService sut)
        {
            mockRepository
                .Setup(x => x.GetUsageData())
                .Returns(Task.FromResult(dataPoints));

            var result = await sut.GetDataPoints();

            Assert.AreEqual(result, dataPoints.OrderBy(d => d));
            mockRepository.Verify(x => x.GetUsageData(), Times.Once);
        }

        [Test, AutoMoqData]
        public async Task GetDaysRemaining_GivenValidData_Succeeds(
            [Frozen] Mock<IToiletRepository> mockRepository,
            IEnumerable<DateTime> dataPoints,
            int rollsRemaining,
            ToiletService sut)
        {
            dataPoints = dataPoints.OrderBy(d => d);

            mockRepository
                .Setup(x => x.GetUsageData())
                .Returns(Task.FromResult(dataPoints));

            mockRepository
                .Setup(x => x.GetNumberOfRollsRemaining())
                .Returns(Task.FromResult(rollsRemaining));

            var result = await sut.GetDaysRemaining();

            var daysRemaining = sut.CalculateDaysRemaining(rollsRemaining, dataPoints);

            Assert.AreEqual(result, daysRemaining);
        }


    }
}
