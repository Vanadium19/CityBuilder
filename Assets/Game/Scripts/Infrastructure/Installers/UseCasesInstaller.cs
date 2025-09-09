using UseCases.Gameplay.Cases;
using UseCases.Gameplay.Services;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class UseCasesInstaller : MonoInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            InstallUseCases(builder);
            InstallServices(builder);
        }

        private void InstallUseCases(IContainerBuilder builder)
        {
            builder.Register<CreateBuildingUseCase>(Lifetime.Singleton);
            builder.Register<RemoveBuildingUseCase>(Lifetime.Singleton);
            builder.Register<UpgradeBuildingUseCase>(Lifetime.Singleton);
        }

        private void InstallServices(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EconomyService>().As<IEconomyService>();
            builder.RegisterEntryPoint<CityService>().As<ICityService>();
            builder.RegisterEntryPoint<IncomeService>().AsSelf();
        }
    }
}