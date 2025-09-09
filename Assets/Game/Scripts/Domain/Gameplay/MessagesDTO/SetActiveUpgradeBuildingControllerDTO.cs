namespace Domain.Gameplay.MessagesDTO
{
    public class SetActiveUpgradeBuildingControllerDTO
    {
        public SetActiveUpgradeBuildingControllerDTO(bool isActive)
        {
            IsActive = isActive;
        }

        public bool IsActive { get; }
    }
}