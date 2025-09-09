using System;
using Domain.Gameplay.Models.Buildings;
using R3;
using VContainer.Unity;

namespace UseCases.Gameplay.Services
{
    public class BuildingService : IInitializable, IDisposable
    {
        private readonly ReactiveProperty<int> _level = new();
        private readonly BuildingModel _model;

        public BuildingService(BuildingModel model)
        {
            _model = model;
        }

        public BuildingType Type => _model.Type;
        public ReadOnlyReactiveProperty<int> Level => _level;

        public void Initialize()
        {
            _level.Value = _model.Level;
            _model.LevelUpgraded += OnLevelUpgraded;
        }

        public void Dispose()
        {
            _model.LevelUpgraded -= OnLevelUpgraded;
        }

        private void OnLevelUpgraded(int level)
        {
            _level.Value = level;
        }
    }
}