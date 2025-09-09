using UnityEngine;
using VContainer;

namespace Infrastructure.Installers
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void Install(IContainerBuilder builder);
    }
}