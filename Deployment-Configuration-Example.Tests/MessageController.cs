using NUnit.Framework;
using Deployment_Configuration_Example.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Deployment_Configuration_Example.Tests
{
    public class Tests
    {
        Mock<ILogger<MessageController>> _logger;
        Mock<IConfiguration> _config;
        Mock<IConfigurationSection> _configSection;

        [SetUp]
        public void Setup()
        {
            _configSection = new Mock<IConfigurationSection>();
            _config = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<MessageController>>();
        }

        [Test]
        public void Test1()
        {
            // Arrange
            _config.Setup(c => c.GetSection("TheMessage")).Returns(_configSection.Object);
            _configSection.Setup(cs => cs.Value).Returns("This is a test");

            var controller = new MessageController(_logger.Object, _config.Object);

            // Act
            var response = controller.Get();

            // Assert
            Assert.AreEqual("This is a test", response);
        }
    }
}