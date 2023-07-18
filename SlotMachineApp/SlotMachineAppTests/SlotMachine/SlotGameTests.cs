using NUnit.Framework;
using Moq;
using SlotMachineApp.Accounts;
using SlotMachineApp.SlotMachine.Spins;
using System.Collections.Generic;

namespace SlotMachineApp.SlotMachine.Tests
{
    [TestFixture]
    public class SlotGameTests
    {
        private Mock<ISlotGameConfig> mockConfig;
        private Mock<IAccountGameService> mockAccount;
        private SlotGame slotGame;

        [SetUp]
        public void Setup()
        {
            mockConfig = new Mock<ISlotGameConfig>();
            mockAccount = new Mock<IAccountGameService>();
            slotGame = new SlotGame(mockConfig.Object, mockAccount.Object);
        }

        [Test]
        public void ShouldReturnNullWhenBetAmountIsInvalid()
        {
            // Arrange
            decimal betAmount = 0;

            // Act
            SpinResult? result = slotGame.Spin(betAmount);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ShouldReturnAValidResultWithValidBetAmount()
        {
            // Arrange
            decimal betAmount = 10;
            mockAccount.Setup(a => a.Balance).Returns(20);

            // Act
            SpinResult? result = slotGame.Spin(betAmount);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldDebitBalanceOnceWithValidBetAmount()
        {
            // Arrange
            decimal betAmount = 10;
            mockAccount.Setup(a => a.Balance).Returns(20);

            // Act
            slotGame.Spin(betAmount);

            // Assert
            mockAccount.Verify(a => a.DebitBalance(betAmount), Times.Once);
        }
    }
}
