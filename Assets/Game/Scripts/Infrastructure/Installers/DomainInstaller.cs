using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using Domain.Gameplay.Models.Wallet;
using UnityEngine;
using VContainer;

namespace Infrastructure.Installers
{
    public class DomainInstaller : MonoInstaller
    {
        [SerializeField] private int _startMoney = 100;

        public override void Install(IContainerBuilder builder)
        {
            InstallWallet(builder);
            InstallCity(builder);
        }

        private void InstallWallet(IContainerBuilder builder)
        {
            builder.Register<WalletModel>(Lifetime.Singleton)
                .WithParameter(_startMoney);
        }

        private void InstallCity(IContainerBuilder builder)
        {
            builder.Register<CityModel>(Lifetime.Singleton);
            builder.Register<BuildingModelFactory>(Lifetime.Singleton);
        }
    }
}