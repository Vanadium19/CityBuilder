using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.MessagesDTO
{
    public class SetConfigInBuilderControllerDTO
    {
        public SetConfigInBuilderControllerDTO(BuildingConfig config)
        {
            Config = config;
        }

        public BuildingConfig Config { get; }
    }
}