using System;

namespace Domain.Gameplay.Models.Buildings
{
    public class BuildingModelFactory
    {
        public BuildingModel Create(BuildingConfig config)
        {
            if (!config)
                throw new ArgumentNullException(nameof(config));

            return new BuildingModel(config);
        }
    }
}