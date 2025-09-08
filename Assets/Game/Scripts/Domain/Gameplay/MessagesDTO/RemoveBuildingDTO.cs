using UnityEngine;

namespace Domain.Gameplay.MessagesDTO
{
    public class RemoveBuildingDTO
    {
        public RemoveBuildingDTO(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}