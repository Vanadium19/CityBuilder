using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Grid;
using UnityEngine;

namespace Domain.Gameplay.Models.City
{
    public class CityModel
    {
        public const int CellSize = 3;

        private readonly Dictionary<GridPosition, BuildingModel> _buildings = new();

        public event Action<GridPosition, BuildingModel> BuildingAdded;
        public event Action<GridPosition, BuildingModel> BuildingRemoved;

        public IReadOnlyDictionary<GridPosition, BuildingModel> BuildingsToPositions => _buildings;
        public IEnumerable<BuildingModel> Buildings => _buildings.Values;
        public int BuildingsCount => _buildings.Count;

        public bool AddBuilding(BuildingModel building, GridPosition position)
        {
            if (_buildings.Values.Contains(building))
                return false;

            if (!_buildings.TryAdd(position, building))
                return false;

            building.SetGridPosition(position);
            BuildingAdded?.Invoke(position, building);
            return true;
        }

        public bool RemoveBuilding(GridPosition position)
        {
            if (!_buildings.Remove(position, out BuildingModel building))
                return false;

            BuildingRemoved?.Invoke(position, building);
            return true;
        }
    }
}