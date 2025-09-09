using System;

namespace Domain.Gameplay.Models.City
{
    [Serializable]
    public struct CityPosition
    {
        public CityPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}