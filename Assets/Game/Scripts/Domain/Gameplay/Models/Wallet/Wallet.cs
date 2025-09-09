using System;
using UnityEngine;

namespace Domain.Gameplay.Models.Wallet
{
    public class WalletModel
    {
        private int _money;

        public WalletModel(int money)
        {
            _money = money;
        }

        public event Action<int> MoneyChanged;

        public int CurrentMoney => _money;

        public void AddMoney(int amount)
        {
            if (amount <= 0)
                return;

            _money += amount;
            MoneyChanged?.Invoke(_money);
        }

        public void RemoveMoney(int amount)
        {
            if (amount <= 0)
                return;

            if (_money < amount)
                return;

            _money -= amount;
            MoneyChanged?.Invoke(_money);
        }
    }
}