using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using Domain.Gameplay.Models.Wallet;
using MessagePipe;
using Presentation.Gameplay.Controllers;
using Presentation.Gameplay.Presenters;
using Presentation.Gameplay.View;
using Presentation.Presentation.View;
using UnityEngine;
using UseCases.Gameplay.Cases;
using UseCases.Gameplay.Services;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private BuildingView[] _buildingViews;
        [SerializeField] private Transform _buildingsContainer;
        [SerializeField] private MoneyView _moneyView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();

            InstallWallet(builder);
            InstallCity(builder);

            InstallUseCases(builder);
            InstallServices(builder);

            InstallPresenters(builder);
            InstallControllers(builder);
            InstallView(builder);
        }

        private void InstallWallet(IContainerBuilder builder)
        {
            builder.Register<WalletModel>(Lifetime.Singleton)
                .WithParameter(100);
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
            builder.RegisterEntryPoint<IncomeService>().AsSelf();
        }

        private void InstallPresenters(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BuildingsPresenter>();
            builder.RegisterEntryPoint<UpgradeBuildingsPresenter>();
            builder.RegisterEntryPoint<MoneyPresenter>();
            builder.Register<BuildingPresenterFactory>(Lifetime.Singleton);
        }

        private void InstallControllers(IContainerBuilder builder)
        {
            //TODO: Убрать AsSelf и подписать на шину
            builder.RegisterEntryPoint<CreateBuildingController>()
                .WithParameter(Camera.main);

            builder.RegisterEntryPoint<RemoveBuildingController>()
                .WithParameter(Camera.main);

            builder.RegisterEntryPoint<UpgradeBuildingController>()
                .WithParameter(Camera.main);
        }

        private void InstallView(IContainerBuilder builder)
        {
            builder.RegisterInstance(_buildingViews);
            builder.Register<BuildingViewFactory>(Lifetime.Singleton).WithParameter(_buildingsContainer);
            builder.RegisterInstance(_moneyView);
        }
    }
}