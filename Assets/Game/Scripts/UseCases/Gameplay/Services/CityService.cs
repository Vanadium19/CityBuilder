using System;
using System.Collections.Generic;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using Domain.Gameplay.Models.Grid;
using ObservableCollections;
using UnityEngine;
using VContainer.Unity;

namespace UseCases.Gameplay.Services
{
    public class CityService : IInitializable, IDisposable
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

        private void OnBuildingAdded(GridPosition position, BuildingModel building)
        {
            var worldPosition = GridPositionToWorldPosition(position);

            var service = new BuildingService(building);
            _buildings.Add(worldPosition, service);
            service.Initialize();
        }

        private void OnBuildingRemoved(GridPosition position, BuildingModel building)
        {
            var worldPosition = GridPositionToWorldPosition(position);

            if (_buildings.Remove(worldPosition, out BuildingService service))
                service.Dispose();
        }

        //TODO: Вынести в Utils
        private Vector3 GridPositionToWorldPosition(GridPosition position)
        {
            var cellSize = CityModel.CellSize;
            var factor = cellSize / 2f;

            var x = position.X * cellSize + factor;
            var z = position.Y * cellSize + factor;

            return new Vector3(x, 0, z);
        }
    }
}