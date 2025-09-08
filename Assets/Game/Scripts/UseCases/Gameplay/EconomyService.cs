using System;
using Domain.Gameplay.Models.Wallet;
using R3;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class EconomyService : IInitializable, IDisposable
    {
        private readonly WalletModel _wallet;
        private readonly ReactiveProperty<int> _money;

        public EconomyService(WalletModel wallet)
        {
            _wallet = wallet;
            _money = new ReactiveProperty<int>(_wallet.CurrentMoney);
        }

        public Observable<int> Money => _money;

        public void Initialize()
        {
            _wallet.MoneyChanged += OnMoneyChanged;
        }

        public void Dispose()
        {
            _wallet.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int amount)
        {
            _money.Value = amount;
        }
    }
}