using Domain.Gameplay.Models.Wallet;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.Gameplay.Tests.Wallet
{
    public class WalletModelTests
    {
        private WalletModel _wallet;

        [SetUp]
        public void SetUp()
        {
            _wallet = new WalletModel(0);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void WhenAddMoney_ItAdded(int multiplier)
        {
            //Arrange
            var calls = 0;
            var amount = 3;

            //Act
            _wallet.MoneyChanged += _ => calls++;

            for (int i = 0; i < multiplier; i++)
                _wallet.AddMoney(amount);

            amount *= multiplier;

            //Assert
            _wallet.CurrentMoney.Should().Be(amount);
            calls.Should().Be(multiplier);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void WhenRemoveMoney_ItRemoved(int multiplier)
        {
            //Arrange
            var calls = 0;
            var amount = 3;

            var startMoney = 10;
            var endMoney = startMoney - amount * multiplier;

            //Act
            _wallet.AddMoney(startMoney);
            _wallet.MoneyChanged += _ => calls++;

            for (int i = 0; i < multiplier; i++)
                _wallet.RemoveMoney(amount);

            //Assert
            _wallet.CurrentMoney.Should().Be(endMoney);
            calls.Should().Be(multiplier);
        }

        [TestCase(0)]
        [TestCase(-5)]
        public void WhenAddWrongAmount_ItNotAdded(int amount)
        {
            //Arrange
            var invoked = false;
            var money = _wallet.CurrentMoney;

            //Act
            _wallet.MoneyChanged += _ => invoked = true;
            _wallet.AddMoney(amount);

            //Assert
            _wallet.CurrentMoney.Should().Be(money);
            invoked.Should().BeFalse();
        }

        [TestCase(10)]
        [TestCase(-5)]
        [TestCase(0)]
        public void WhenRemoveWrongAmount_ItNotRemoved(int amount)
        {
            //Arrange
            var invoked = false;
            _wallet.AddMoney(3);
            var money = _wallet.CurrentMoney;

            //Act
            _wallet.MoneyChanged += _ => invoked = true;
            _wallet.RemoveMoney(amount);

            //Assert
            _wallet.CurrentMoney.Should().Be(money);
            invoked.Should().BeFalse();
        }
    }
}