using System;
using System.Collections.Generic;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using Domain.Gameplay.Models.Wallet;
using R3;
using VContainer.Unity;

namespace UseCases.Gameplay.Services
{
    public class IncomeService : IInitializable, IDisposable
    {
        private readonly CityModel _city;
        private readonly WalletModel _wallet;

        private readonly Dictionary<BuildingModel, IDisposable> _disposables = new();
        private readonly Subject<int> _incomeStream = new();

        private IDisposable _incomeStreamDisposable;

        public IncomeService(CityModel city, WalletModel wallet)
        {
            _city = city;
            _wallet = wallet;
        }

        public void Initialize()
        {
            _city.BuildingAdded += OnBuildingAdded;
            _city.BuildingRemoved += OnBuildingRemoved;

            _incomeStreamDisposable = _incomeStream.Subscribe(_wallet.AddMoney);
        }

        private void OnBuildingAdded(CityPosition position, BuildingModel building)
        {
            var disposable = Observable.Interval(TimeSpan.FromSeconds(building.IncomeDelay))
                .Select(_ => building.Income)
                .Subscribe(income => _incomeStream.OnNext(income));

            _disposables[building] = disposable;
        }

        private void OnBuildingRemoved(CityPosition position, BuildingModel building)
        {
            if (_disposables.Remove(building, out IDisposable disposable))
                disposable.Dispose();
        }

        public void Dispose()
        {
            _city.BuildingAdded -= OnBuildingAdded;
            _city.BuildingRemoved -= OnBuildingRemoved;
            _incomeStreamDisposable.Dispose();

            foreach (var disposable in _disposables.Values)
                disposable.Dispose();

            _disposables.Clear();
        }
    }
}