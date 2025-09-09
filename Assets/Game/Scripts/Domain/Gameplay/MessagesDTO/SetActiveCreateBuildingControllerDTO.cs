namespace Domain.Gameplay.MessagesDTO
{
    public class SetActiveCreateBuildingControllerDTO
    {
        public SetActiveCreateBuildingControllerDTO(bool isActive)
        {
            IsActive = isActive;
        }

        public bool IsActive { get; }
    }
}