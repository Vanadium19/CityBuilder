using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private MonoInstaller[] _monoInstallers;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();

            foreach (var monoInstaller in _monoInstallers)
                monoInstaller.Install(builder);
        }
    }
}