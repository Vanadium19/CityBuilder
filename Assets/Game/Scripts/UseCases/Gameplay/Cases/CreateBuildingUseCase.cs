using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using Domain.Gameplay.Models.Grid;
using Domain.Gameplay.Models.Wallet;
using UnityEngine;

namespace UseCases.Gameplay.Cases
{
    public class CreateBuildingUseCase
    {
        private readonly CityModel _city;
        private readonly WalletModel _wallet;
        private readonly BuildingModelFactory _factory;

        public CreateBuildingUseCase(CityModel city, WalletModel wallet, BuildingModelFactory factory)
        {
            _city = city;
            _wallet = wallet;
            _factory = factory;
        }

        public void Handle(CreateBuildingDTO dto)
        {
            var cost = dto.Config.Cost;

            if (cost > _wallet.CurrentMoney)
                return;

            var position = WorldPositionToGridPosition(dto.Position);
            var building = _factory.Create(dto.Config);

            if (_city.AddBuilding(building, position))
                _wallet.RemoveMoney(cost);
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