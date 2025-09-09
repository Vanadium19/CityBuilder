using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.City;
using Domain.Gameplay.Models.Grid;
using UnityEngine;

namespace UseCases.Gameplay.Cases
{
    public class RemoveBuildingUseCase
    {
        private readonly CityModel _city;

        public RemoveBuildingUseCase(CityModel city)
        {
            _city = city;
        }

        public void Handle(RemoveBuildingDTO dto)
        {
            var position = WorldPositionToGridPosition(dto.Position);

            _city.RemoveBuilding(position);
        }

        //TODO: Вынести в utils
        private GridPosition WorldPositionToGridPosition(Vector3 position)
        {
            var cellSize = CityModel.CellSize;

            var x = Mathf.FloorToInt(position.x / cellSize);
            var y = Mathf.FloorToInt(position.z / cellSize);

            return new GridPosition(x, y);
        }
    }
}