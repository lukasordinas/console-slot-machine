using NUnit.Framework;
using SlotMachineApp.Accounts;
using System.Security.Principal;

namespace SlotMachineApp.Accounts.Tests
{
    [TestFixture]
    public class AccountTests
    {
        [Test]
        public void ShouldHaveZeroBalanceOnANewlyCreatedAccount()
        {
            // Arrange
            Account account = new();

            // Assert
            Assert.That(account.Balance, Is.EqualTo(0));
        }

        [Test]
        public void ShouldReturnTrueWhenDepositingValidAmount()
        {
            // Arrange
            Account account = new();
            decimal amount = 100;

            // Act
            bool result = account.Deposit(amount);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(account.Balance, Is.EqualTo(amount));
        }

        [Test]
        public void ShouldReturnFalseWhenDepositingNegativeAmount()
        {
            // Arrange
            Account account = new();
            decimal amount = -50;

            // Act
            bool result = account.Deposit(amount);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(account.Balance, Is.EqualTo(0));
        }

        [Test]
        public void ShouldReturnTrueWhenCreditingValidAmount()
        {
            // Arrange
            Account account = new();
            decimal amount = 200;

            // Act
            bool result = account.CreditBalance(amount);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(account.Balance, Is.EqualTo(amount));
        }

        [Test]
        public void ShouldReturnTrueWhenDebitingBalanceAndHaveEnoughMoney()
        {
            // Arrange
            Account account = new();
            decimal initialBalance = 500;
            decimal debitAmount = 200;
            account.CreditBalance(initialBalance);

            // Act
            bool result = account.DebitBalance(debitAmount);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(account.Balance, Is.EqualTo(initialBalance - debitAmount));
        }

        [Test]
        public void ShouldReturnFalseWhenDebitingBalanceAndDontHaveEnoughMoney()
        {
            // Arrange
            Account account = new();
            decimal initialBalance = 100;
            decimal debitAmount = 200;
            account.CreditBalance(initialBalance);

            // Act
            bool result = account.DebitBalance(debitAmount);

            // Assert
            Assert.IsFalse(result);
            Assert.That(account.Balance, Is.EqualTo(initialBalance));
        }
    }
}