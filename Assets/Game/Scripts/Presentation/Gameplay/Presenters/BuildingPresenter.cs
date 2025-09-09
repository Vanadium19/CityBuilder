using System;
using Domain.Gameplay.Models.Buildings;
using Presentation.Presentation.View;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class BuildingPresenter : IInitializable, IDisposable
    {
        private readonly BuildingModel _model;
        private readonly BuildingView _view;

        public BuildingPresenter(BuildingModel model, BuildingView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
        }
    }
}