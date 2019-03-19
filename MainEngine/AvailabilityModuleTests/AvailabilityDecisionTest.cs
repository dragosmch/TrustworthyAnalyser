using AvailabilityModule;
using LibraryModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AvailabilityModuleTests
{
    [TestClass]
    public class AvailabilityDecisionTest
    {
        private Mock<IAvailabilityRunner> _availabilityRunnerMock;
        private AvailabilityDecision _availabilityDecision;

        [TestInitialize]
        public void TestSetUp()
        {
            _availabilityRunnerMock = new Mock<IAvailabilityRunner>();
            _availabilityDecision = new AvailabilityDecision(_availabilityRunnerMock.Object);
        }

        [TestMethod]
        public void WhenGetAvailability_ExpectNotNull()
        {
            // Arrange
            _availabilityRunnerMock.Setup(runner =>
                runner.RunExecutableMultipleTimes(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(It.IsAny<int>());

            // Act
            var availabilityResult = _availabilityDecision
                .GetAvailabilityDecision(It.IsAny<string>(), It.IsAny<AnalysisMode>());

            // Assert
            Assert.IsNotNull(availabilityResult);
        }

        [TestMethod]
        public void WhenGetAvailability_ExpectAvailabilityScoreBetweenMinusOneAndOneInclusive()
        {
            // Arrange
            _availabilityRunnerMock.Setup(runner =>
                runner.RunExecutableMultipleTimes(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(It.IsAny<int>());

            // Act
            int resultAvailabilityScore = _availabilityDecision
                .GetAvailabilityDecision(It.IsAny<string>(), It.IsAny<AnalysisMode>()).AvailabilityScore;

            // Assert
            Assert.IsTrue(resultAvailabilityScore >= -1);
            Assert.IsTrue(resultAvailabilityScore <= 1);
        }

        // AvailabilityDecision should pass on the number of successful runs coming from AvailabilityScore Runner
        [TestMethod]
        public void WhenGetAvailabilityAndRunnerReturnsFive_ExpectFiveSuccessfulRuns()
        {
            // Arrange
            int five = 5;
            _availabilityRunnerMock.Setup(runner =>
                runner.RunExecutableMultipleTimes(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(five);

            // Act
            int resultNoOfSuccessfulRuns = _availabilityDecision
                .GetAvailabilityDecision(It.IsAny<string>(), It.IsAny<AnalysisMode>()).AvailabilityNoOfSuccessfulRuns;

            // Assert
            Assert.AreEqual(five, resultNoOfSuccessfulRuns);
        }

        [TestMethod]
        public void WhenGetAvailability_ExpectNoOfRunsLargerThanZero()
        {
            // Arrange
            _availabilityRunnerMock.Setup(runner =>
                runner.RunExecutableMultipleTimes(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(It.IsAny<int>());
            // Act
            int resultNoOfRuns = _availabilityDecision
                .GetAvailabilityDecision(It.IsAny<string>(), AnalysisMode.Advanced).AvailabilityNoOfRuns;

            // Assert
            Assert.IsTrue(resultNoOfRuns > 0);
        }

        [TestMethod]
        public void WhenGetAvailability_ExpectNoOfSuccessfulRunsLargerThanMinusOne()
        {
            // Arrange
            _availabilityRunnerMock.Setup(runner =>
                runner.RunExecutableMultipleTimes(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(It.IsAny<int>());
            // Act
            int resultNoOfSuccessfulRuns = _availabilityDecision
                .GetAvailabilityDecision(It.IsAny<string>(), AnalysisMode.Advanced).AvailabilityNoOfSuccessfulRuns;

            // Assert
            Assert.IsTrue(resultNoOfSuccessfulRuns > -1);
        }
        
        [TestMethod]
        public void WhenGetAvailabilityAndBasicMode_ExpectThreeNoOfRuns()
        {
            // Arrange
            _availabilityRunnerMock.Setup(runner =>
                runner.RunExecutableMultipleTimes(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(It.IsAny<int>());
            // Act
            int resultNoOfRuns = _availabilityDecision
                .GetAvailabilityDecision(It.IsAny<string>(), AnalysisMode.Basic).AvailabilityNoOfRuns;

            // Assert
            Assert.AreEqual(3, resultNoOfRuns);
        }

        [TestMethod]
        public void WhenGetAvailabilityAndMediumMode_ExpectFiveNoOfRuns()
        {
            // Arrange
            _availabilityRunnerMock.Setup(runner =>
                runner.RunExecutableMultipleTimes(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(It.IsAny<int>());
            // Act
            int resultNoOfRuns = _availabilityDecision
                .GetAvailabilityDecision(It.IsAny<string>(), AnalysisMode.Medium).AvailabilityNoOfRuns;

            // Assert
            Assert.AreEqual(5, resultNoOfRuns);
        }

        [TestMethod]
        public void WhenGetAvailabilityAndAdvancedMode_ExpectTenNoOfRuns()
        {
            // Arrange
            _availabilityRunnerMock.Setup(runner =>
                runner.RunExecutableMultipleTimes(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(It.IsAny<int>());
            // Act
            int resultNoOfRuns = _availabilityDecision
                .GetAvailabilityDecision(It.IsAny<string>(), AnalysisMode.Advanced).AvailabilityNoOfRuns;

            // Assert
            Assert.AreEqual(10, resultNoOfRuns);
        }
    }
}
