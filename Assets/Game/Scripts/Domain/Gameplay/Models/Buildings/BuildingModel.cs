using System;
using Domain.Gameplay.Models.City;

namespace Domain.Gameplay.Models.Buildings
{
    public class BuildingModel
    {
        internal const int StartLevel = 1;
        internal const int MaxLevel = 3;

        private readonly BuildingConfig _config;

        private CityPosition _cityPositon;
        private int _level;

        public event Action<int> LevelUpgraded;

        public bool CanUpgrade => _level < MaxLevel;
        public int Level => _level;

        public int Cost => _config.Cost * _level;
        public int Income => _config.Income * _level;
        public float IncomeDelay => _config.IncomeDelay;

        public BuildingModel(BuildingConfig config)
        {
            if (!config)
                throw new ArgumentNullException(nameof(config));

            _config = config;
            _level = StartLevel;
        }

        public BuildingType Type => _config.Type;
        public CityPosition Position => _cityPositon;

        public void Upgrade()
        {
            if (!CanUpgrade)
                return;

            _level++;
            LevelUpgraded?.Invoke(_level);
        }

        internal void SetCityPosition(CityPosition value)
        {
            _cityPositon = value;
        }
    }
}