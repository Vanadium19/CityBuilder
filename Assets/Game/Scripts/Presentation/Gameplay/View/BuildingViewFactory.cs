using System.Collections.Generic;
using System.Linq;
using Domain.Gameplay.Models.Buildings;
using Presentation.Presentation.View;
using UnityEngine;

namespace Presentation.Gameplay.View
{
    public class BuildingViewFactory
    {
        private readonly Dictionary<BuildingType, BuildingView> _catalog;
        private readonly Transform _container;

        public BuildingViewFactory(BuildingView[] views, Transform container)
        {
            _catalog = views.ToDictionary(key => key.Type, value => value);
            _container = container;
        }

        public BuildingView Create(BuildingType type, Vector3 position)
        {
            if (!_catalog.TryGetValue(type, out BuildingView prefab))
                return null;

            return Object.Instantiate(prefab, position, Quaternion.identity, _container);
        }
    }
}