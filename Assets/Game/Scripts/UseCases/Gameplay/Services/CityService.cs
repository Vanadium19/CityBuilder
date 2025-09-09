using System;
using System.Collections.Generic;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using ObservableCollections;
using UnityEngine;
using Utils.Extenshions;
using VContainer.Unity;

namespace UseCases.Gameplay.Services
{
    public class CityService : IInitializable, IDisposable, ICityService
    {
        private readonly CityModel _cityModel;

        private readonly ObservableDictionary<Vector3, BuildingService> _buildings = new();

        public CityService(CityModel cityModel)
        {
            _cityModel = cityModel;
        }

        public IReadOnlyObservableDictionary<Vector3, BuildingService> Buildings => _buildings;

        public void Initialize()
        {
            _cityModel.BuildingAdded += OnBuildingAdded;
            _cityModel.BuildingRemoved += OnBuildingRemoved;
        }

        public void Dispose()
        {
            _cityModel.BuildingAdded -= OnBuildingAdded;
            _cityModel.BuildingRemoved -= OnBuildingRemoved;
        }

        private void OnBuildingAdded(CityPosition position, BuildingModel building)
        {
            var worldPosition = _cityModel.CityPositionToWorldPosition(position);

            var service = new BuildingService(building);
            _buildings.Add(worldPosition, service);
            service.Initialize();
        }

        private void OnBuildingRemoved(CityPosition position, BuildingModel building)
        {
            var worldPosition = _cityModel.CityPositionToWorldPosition(position);

            if (_buildings.Remove(worldPosition, out BuildingService service))
                service.Dispose();
        }
    }
}