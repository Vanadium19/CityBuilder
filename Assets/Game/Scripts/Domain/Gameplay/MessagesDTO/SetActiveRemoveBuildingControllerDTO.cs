namespace Domain.Gameplay.MessagesDTO
{
    public class SetActiveRemoveBuildingControllerDTO
    {
        public SetActiveRemoveBuildingControllerDTO(bool isActive)
        {
            IsActive = isActive;
        }

        public bool IsActive { get; }
    }
}