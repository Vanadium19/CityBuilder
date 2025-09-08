using UnityEngine;

namespace Domain.Gameplay.MessagesDTO
{
    public class UpgradeBuildingDTO
    {
        public UpgradeBuildingDTO(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}