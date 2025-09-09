using System;
using UnityEngine;

namespace Domain.Gameplay.Models.Wallet
{
    public class WalletModel
    {
        private int _money;

        public event Action<int> MoneyChanged;

        public int CurrentMoney => _money;

        public void AddMoney(int amount)
        {
            if (amount <= 0)
                return;

            _money += amount;
            MoneyChanged?.Invoke(_money);
            Debug.Log($"Money added! current money: {_money}");
        }

        public void RemoveMoney(int amount)
        {
            if (amount <= 0)
                return;

            if (_money < amount)
                return;

            Debug.Log($"Money removed! current money: {_money}");
            _money -= amount;
            MoneyChanged?.Invoke(_money);
        }
    }
}