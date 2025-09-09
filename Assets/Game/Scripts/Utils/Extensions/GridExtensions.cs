using Domain.Gameplay.Models.City;
using UnityEngine;

namespace Utils.Extenshions
{
    public static class CityExtensions
    {
        private const float PositionY = 0.5f;

        public static CityPosition WorldPositionToCityPosition(this CityModel city, Vector3 position)
        {
            var cellSize = CityModel.CellSize;

            var x = Mathf.FloorToInt(position.x / cellSize);
            var y = Mathf.FloorToInt(position.z / cellSize);

            return new CityPosition(x, y);
        }

        public static Vector3 CityPositionToWorldPosition(this CityModel city, CityPosition position)
        {
            var cellSize = CityModel.CellSize;
            var factor = cellSize / 2f;

            var x = position.X * cellSize + factor;
            var z = position.Y * cellSize + factor;

            return new Vector3(x, PositionY, z);
        }
    }
}