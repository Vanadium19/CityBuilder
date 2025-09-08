using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using Domain.Gameplay.Models.Grid;
using Domain.Gameplay.Models.Wallet;
using UnityEngine;

namespace UseCases.Gameplay
{
    public class UpgradeBuildingUseCase
    {
        private readonly CityModel _city;
        private readonly WalletModel _wallet;

        public UpgradeBuildingUseCase(CityModel city, WalletModel wallet)
        {
            _city = city;
            _wallet = wallet;
        }

        public void Handle(UpgradeBuildingDTO dto)
        {
            var position = WorldPositionToGridPosition(dto.Position);

            if (!_city.BuildingsToPositions.TryGetValue(position, out BuildingModel buildings))
                return;

            if (!buildings.CanUpgrade)
                return;

            var cost = buildings.Cost;

            if (cost > _wallet.CurrentMoney)
                return;

            buildings.Upgrade();
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