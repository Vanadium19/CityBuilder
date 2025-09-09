using Domain.Gameplay.Models.Buildings;
using UnityEngine;

namespace Domain.Gameplay.MessagesDTO
{
    public class CreateBuildingDTO
    {
        public CreateBuildingDTO(BuildingConfig config, Vector3 position)
        {
            Config = config;
            Position = position;
        }

        public BuildingConfig Config { get; }
        public Vector3 Position { get; }
    }
}