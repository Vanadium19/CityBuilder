using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using Domain.Gameplay.Models.Wallet;
using MessagePipe;
using Presentation.Gameplay.Presenters;
using Presentation.Gameplay.View;
using Presentation.Presentation.View;
using UnityEngine;
using UseCases.Gameplay;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private BuildingView[] _buildingViews;
        [SerializeField] private Transform _buildingsContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();

            InstallWallet(builder);
            InstallCity(builder);

            InstallUseCases(builder);
            InstallServices(builder);

            InstallPresenter(builder);
            InstallView(builder);
        }

        private void InstallWallet(IContainerBuilder builder)
        {
            builder.Register<WalletModel>(Lifetime.Singleton);
        }

        private void InstallCity(IContainerBuilder builder)
        {
            builder.Register<CityModel>(Lifetime.Singleton);
            builder.Register<BuildingModelFactory>(Lifetime.Singleton);
        }

        private void InstallUseCases(IContainerBuilder builder)
        {
            builder.Register<CreateBuildingUseCase>(Lifetime.Singleton);
            builder.Register<RemoveBuildingUseCase>(Lifetime.Singleton);
            builder.Register<UpgradeBuildingUseCase>(Lifetime.Singleton);
        }

        private void InstallServices(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EconomyService>().AsSelf();
            builder.RegisterEntryPoint<CityService>().AsSelf();
        }

        private void InstallPresenter(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CreateBuildingsPresenter>();
        }

        private void InstallView(IContainerBuilder builder)
        {
            builder.RegisterInstance(_buildingViews);
            builder.Register<BuildingViewFactory>(Lifetime.Singleton).WithParameter(_buildingsContainer);
        }
    }
}