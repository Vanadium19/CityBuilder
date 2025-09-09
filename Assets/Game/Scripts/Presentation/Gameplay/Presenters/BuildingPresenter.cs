using System;
using Domain.Gameplay.Models.Buildings;
using Presentation.Presentation.View;
using R3;
using UseCases.Gameplay.Services;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class BuildingPresenter : IInitializable, IDisposable
    {
        private readonly BuildingService _building;
        private readonly BuildingView _view;

        private IDisposable _disposable;

        public BuildingPresenter(BuildingService building, BuildingView view)
        {
            _building = building;
            _view = view;
        }

        public void Initialize()
        {
            _disposable = _building.Level.Subscribe(_view.UpdateLevel);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}