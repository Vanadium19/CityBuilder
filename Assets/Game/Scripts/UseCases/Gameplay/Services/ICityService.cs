using ObservableCollections;
using UnityEngine;

namespace UseCases.Gameplay.Services
{
    public interface ICityService
    {
        public IReadOnlyObservableDictionary<Vector3, BuildingService> Buildings { get; }
    }
}