using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Configuration;

namespace SlotMachineApp.SlotMachine.Tests
{
    [TestFixture]
    public class SlotGameConfigTests
    {
        [Test]
        public void ShouldInitialiseCorrectlyWhenGivenAValidFile()
        {
            // Arrange
            string configFilePath = Path.Combine("GameConfigs", "TestSlots.json");

            // Act
            var slotGameConfig = new SlotGameConfig(configFilePath);

            // Assert
            Assert.That(slotGameConfig.GameTitle, Is.EqualTo("TestSlots"));
            Assert.That(slotGameConfig.Symbols.Count(), Is.EqualTo(4));
            Assert.That(slotGameConfig.Rows, Is.EqualTo(4));
            Assert.That(slotGameConfig.Columns, Is.EqualTo(3));
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenIncorrectFilePath()
        {
            // Arrange
            string configFilePath = Path.Combine("GameConfigs", "MissingFile.json");

            // Act + Assert
            Assert.Throws<InvalidOperationException>(() => new SlotGameConfig(configFilePath));
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenGivenMisconfiguredFile()
        {
            // Arrange
            string configFilePath = Path.Combine("GameConfigs", "TestSlotsMisconfigured.json"); // probabilities don't add up to 1

            // Act + Assert
            Assert.Throws<InvalidOperationException>(() => new SlotGameConfig(configFilePath));
        }
    }
}
