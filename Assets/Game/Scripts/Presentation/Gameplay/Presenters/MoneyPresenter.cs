using System;
using Presentation.Gameplay.View;
using R3;
using UnityEngine;
using UseCases.Gameplay.Services;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class MoneyPresenter : IStartable, IDisposable
    {
        private readonly IEconomyService _service;
        private readonly MoneyView _view;

        private IDisposable _disposable;

        public MoneyPresenter(IEconomyService service, MoneyView view)
        {
            _service = service;
            _view = view;
        }

        public void Start()
        {
            _disposable = _service.Money.Subscribe(OnMoneyChanged);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnMoneyChanged(int money)
        {
            var text = $"{money}$";

            _view.SetMoney(text);
        }
    }
}