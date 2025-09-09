using Presentation.Gameplay.Controllers;
using Presentation.Gameplay.Presenters;
using Presentation.Gameplay.View;
using Presentation.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class PresentationInstaller : MonoInstaller
    {
        [SerializeField] private BuildingView[] _buildingViews;
        [SerializeField] private Transform _buildingsContainer;
        [SerializeField] private MoneyView _moneyView;

        public override void Install(IContainerBuilder builder)
        {
            InstallPresenters(builder);
            InstallControllers(builder);
            InstallView(builder);
        }

        private void InstallPresenters(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CityPresenter>();
            builder.Register<BuildingPresenterFactory>(Lifetime.Singleton);

            builder.RegisterEntryPoint<CreateBuildingPresenter>();
            builder.RegisterEntryPoint<RemoveBuildingPresenter>();
            builder.RegisterEntryPoint<UpgradeBuildingsPresenter>();

            builder.RegisterEntryPoint<MoneyPresenter>();
        }

        private void InstallControllers(IContainerBuilder builder)
        {
            var mainCamera = Camera.main;

            builder.RegisterEntryPoint<CreateBuildingController>()
                .WithParameter(mainCamera);

            builder.RegisterEntryPoint<RemoveBuildingController>()
                .WithParameter(mainCamera);

            builder.RegisterEntryPoint<UpgradeBuildingController>()
                .WithParameter(mainCamera);
        }

        private void InstallView(IContainerBuilder builder)
        {
            builder.RegisterInstance(_buildingViews);
            builder.Register<BuildingViewFactory>(Lifetime.Singleton).WithParameter(_buildingsContainer);
            builder.RegisterInstance(_moneyView);
        }
    }
}