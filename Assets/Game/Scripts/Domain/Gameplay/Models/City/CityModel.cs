using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.Models.City
{
    public class CityModel
    {
        public const int CellSize = 3;

        private readonly Dictionary<CityPosition, BuildingModel> _buildings = new();

        public event Action<CityPosition, BuildingModel> BuildingAdded;
        public event Action<CityPosition, BuildingModel> BuildingRemoved;

        public IReadOnlyDictionary<CityPosition, BuildingModel> BuildingsToPositions => _buildings;
        public IEnumerable<BuildingModel> Buildings => _buildings.Values;
        public int BuildingsCount => _buildings.Count;

        public bool AddBuilding(BuildingModel building, CityPosition position)
        {
            if (_buildings.Values.Contains(building))
                return false;

            if (!_buildings.TryAdd(position, building))
                return false;

            building.SetCityPosition(position);
            BuildingAdded?.Invoke(position, building);
            return true;
        }

        public bool RemoveBuilding(CityPosition position)
        {
            if (!_buildings.Remove(position, out BuildingModel building))
                return false;

            BuildingRemoved?.Invoke(position, building);
            return true;
        }
    }
}