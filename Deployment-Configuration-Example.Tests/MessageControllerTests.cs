using Deployment_Configuration_Example.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Dynamic;

namespace Deployment_Configuration_Example.Tests
{
    public class MessageControllerTests
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
        public void Get_ToTestStandardMessageResponse_RespondsWithMockedMessage()
        {
            // Arrange
            _config.Setup(c => c.GetSection("TheMessage")).Returns(_configSection.Object);
            _configSection.Setup(cs => cs.Value).Returns("This is a test");

            var controller = new MessageController(_logger.Object, _config.Object);

            // Act
            var response = controller.Get();


            // Assert
            Assert.IsNotNull(response);

            try
            {
                var str = JsonConvert.SerializeObject(response);
                dynamic dynResponse = JsonConvert.DeserializeObject(str);

                Assert.IsNotNull(dynResponse.message);
                Assert.AreEqual("This is a test", dynResponse.message.Value);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}