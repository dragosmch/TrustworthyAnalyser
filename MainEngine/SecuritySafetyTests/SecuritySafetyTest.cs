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
    }
}
