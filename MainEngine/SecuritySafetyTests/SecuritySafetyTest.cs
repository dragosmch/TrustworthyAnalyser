using System;
using LibraryModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecuritySafetyModule;

namespace SecuritySafetyTests
{
    [TestClass]
    public class SecuritySafetyTest
    {
        private Mock<ISecuritySafetyRunner> _securitySafetyRunnerMock;
        private SecuritySafetyDecision _securitySafetyDecision;

        [TestInitialize]
        public void TestSetUp()
        {
            _securitySafetyRunnerMock = new Mock<ISecuritySafetyRunner>();
            _securitySafetyDecision = new SecuritySafetyDecision(_securitySafetyRunnerMock.Object);
        }

        [TestMethod]
        public void WhenGetSecuritySafety_ExpectNotNull()
        {
            // Arrange
            _securitySafetyRunnerMock.Setup(runner => runner.GetWinCheckSecResultObject(It.IsAny<string>()))
                .Returns(new WinCheckSecResultObject());

            // Act
            var securitySafetyResult = 
                _securitySafetyDecision.GetSecuritySafetyDecision(It.IsAny<string>(), It.IsAny<AnalysisMode>());

            // Assert
            Assert.IsNotNull(securitySafetyResult);
        }

        [TestMethod]
        public void WhenGetSecuritySafetyAndWinCheckSecObjectNull_ExpectPercentageMinusOne()
        {
            // Arrange
            _securitySafetyRunnerMock.Setup(runner => runner.GetWinCheckSecResultObject(It.IsAny<string>()))
                .Returns((WinCheckSecResultObject)null);

            // Act
            var safetySecurityPercentage =
                _securitySafetyDecision.GetSecuritySafetyDecision(It.IsAny<string>(), It.IsAny<AnalysisMode>())
                    .SafetyAndSecurityPercentage;

            // Assert
            Assert.AreEqual(-1, safetySecurityPercentage);
        }

        [TestMethod]
        public void WhenGetSecuritySafetyAndBasicMode_ExpectPercentageBaseEighty()
        {
            // Arrange
            _securitySafetyRunnerMock.Setup(runner => runner.GetWinCheckSecResultObject(It.IsAny<string>()))
                .Returns((WinCheckSecResultObject)null);

            // Act
            var safetySecurityPercentangeBase =
                _securitySafetyDecision.GetSecuritySafetyDecision(It.IsAny<string>(), AnalysisMode.Basic)
                    .SafetyAndSecurityPercentageBase;

            // Assert
            Assert.AreEqual(80, safetySecurityPercentangeBase);
        }

        [TestMethod]
        public void WhenGetSecuritySafetyAndMediumMode_ExpectPercentageBaseNinetyTwo()
        {
            // Arrange
            _securitySafetyRunnerMock.Setup(runner => runner.GetWinCheckSecResultObject(It.IsAny<string>()))
                .Returns((WinCheckSecResultObject)null);

            // Act
            var safetySecurityPercentangeBase =
                _securitySafetyDecision.GetSecuritySafetyDecision(It.IsAny<string>(), AnalysisMode.Medium)
                    .SafetyAndSecurityPercentageBase;

            // Assert
            Assert.AreEqual(92, safetySecurityPercentangeBase);
        }

        [TestMethod]
        public void WhenGetSecuritySafetyAndAdvancedMode_ExpectPercentageBaseOneHundred()
        {
            // Arrange
            _securitySafetyRunnerMock.Setup(runner => runner.GetWinCheckSecResultObject(It.IsAny<string>()))
                .Returns((WinCheckSecResultObject)null);

            // Act
            var safetySecurityPercentangeBase =
                _securitySafetyDecision.GetSecuritySafetyDecision(It.IsAny<string>(), AnalysisMode.Advanced)
                    .SafetyAndSecurityPercentageBase;

            // Assert
            Assert.AreEqual(100, safetySecurityPercentangeBase);
        }

        [TestMethod]
        public void WhenGetSecuritySafety_ExpectSameWinCheckSecObject()
        {
            // Arrange
            var winCheckSecObject = new WinCheckSecResultObject();
            _securitySafetyRunnerMock.Setup(runner => runner.GetWinCheckSecResultObject(It.IsAny<string>()))
                .Returns(winCheckSecObject);

            // Act
            var securitySafetyResult =
                _securitySafetyDecision.GetSecuritySafetyDecision(It.IsAny<string>(), It.IsAny<AnalysisMode>()).WinCheckSecResultObject;

            // Assert
            Assert.AreSame(winCheckSecObject, securitySafetyResult);
        }
    }
}
