using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.Models.City
{
    public class CityModel
    {
        public const int CellSize = 5;

        private readonly Dictionary<GridPosition, BuildingModel> _buildings = new();

        public event Action<GridPosition, BuildingModel> BuildingAdded;
        public event Action<GridPosition, BuildingModel> BuildingRemoved;

        public IReadOnlyDictionary<GridPosition, BuildingModel> BuildingsToPositions => _buildings;
        public IEnumerable<BuildingModel> Buildings => _buildings.Values;
        public int BuildingsCount => _buildings.Count;

        public void AddBuilding(BuildingModel building, GridPosition position)
        {
            if (_buildings.Values.Contains(building))
                return;

            if (!_buildings.TryAdd(position, building))
                return;

            building.SetGridPosition(position);
            BuildingAdded?.Invoke(position, building);
        }

        public void RemoveBuilding(BuildingModel building)
        {
            var position = building.Position;

            if (!_buildings.TryGetValue(position, out BuildingModel oldBuilding))
                return;

            if (oldBuilding != building)
                return;

            _buildings.Remove(position);
            BuildingRemoved?.Invoke(position, building);
        }
    }
}