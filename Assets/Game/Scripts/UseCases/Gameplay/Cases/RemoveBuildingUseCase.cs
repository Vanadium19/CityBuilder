using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.City;
using Utils.Extenshions;

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
            var position = _city.WorldPositionToCityPosition(dto.Position);

            _city.RemoveBuilding(position);
        }
    }
}