using Deployment_Configuration_Example.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Deployment_Configuration_Example.Tests
{
    public class MessageControllerTests
    {
        Mock<ILogger<MessageController>> _logger;
        Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _config = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<MessageController>>();
        }

        [Test]
        public void Get_ToTestStandardMessageResponse_RespondsWithMockedMessage()
        {
            // Arrange
            var attribTheMessage = new Mock<IConfigurationSection>();
            attribTheMessage.Setup(c => c.Value).Returns("Test Value A");

            var attribConnectionString = new Mock<IConfigurationSection>();
            attribConnectionString.Setup(c => c.Value).Returns("Test Connection String A");

            var attribSettingA = new Mock<IConfigurationSection>();
            attribSettingA.Setup(c => c.Value).Returns("Test Value B");

            var attribLegacyConnectionString = new Mock<IConfigurationSection>();
            attribLegacyConnectionString.Setup(c => c.Value).Returns("Test Connection String B");

            _config.Setup(c => c.GetSection("TheMessage")).Returns(attribTheMessage.Object);
            _config.Setup(c => c.GetSection("ANestedSetting:DatabaseConnection")).Returns(attribConnectionString.Object);
            _config.Setup(c => c.GetSection("SettingA")).Returns(attribSettingA.Object);
            _config.Setup(c => c.GetSection("ConnectionStrings:LegacyConnectionString")).Returns(attribLegacyConnectionString.Object);

            var controller = new MessageController(_logger.Object, _config.Object);

            // Act
            var response = controller.Get();

            // Assert
            Assert.IsNotNull(response);

            try
            {
                var str = JsonConvert.SerializeObject(response);
                dynamic dynResponse = JsonConvert.DeserializeObject(str);

                Assert.IsNotNull(dynResponse.appSettings_json.message);
                Assert.AreEqual("Test Value A", dynResponse.appSettings_json.message.Value);

                Assert.IsNotNull(dynResponse.appSettings_json.connectionString);
                Assert.AreEqual("Test Connection String A", dynResponse.appSettings_json.connectionString.Value);

                Assert.IsNotNull(dynResponse.web_config.setting);
                Assert.AreEqual("Test Value B", dynResponse.web_config.setting.Value);

                Assert.IsNotNull(dynResponse.web_config.connectionString);
                Assert.AreEqual("Test Connection String B", dynResponse.web_config.connectionString.Value);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}