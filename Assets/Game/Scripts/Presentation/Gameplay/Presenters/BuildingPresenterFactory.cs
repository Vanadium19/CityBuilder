using Presentation.Presentation.View;
using UseCases.Gameplay.Services;

namespace Presentation.Gameplay.Presenters
{
    public class BuildingPresenterFactory
    {
        public BuildingPresenter Create(BuildingService building, BuildingView view)
        {
            var presenter = new BuildingPresenter(building, view);
            presenter.Initialize();
            return presenter;
        }
    }
}