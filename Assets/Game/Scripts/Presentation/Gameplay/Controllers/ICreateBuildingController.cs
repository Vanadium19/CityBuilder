using Domain.Gameplay.Models.Buildings;

namespace Presentation.Gameplay.Controllers
{
    public interface ICreateBuildingController
    {
        public void SetBuilding(BuildingConfig config);
        public void SetActive(bool value);
    }
}