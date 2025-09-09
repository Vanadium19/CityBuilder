using System.Collections.Generic;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.City;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;

namespace Domain.Gameplay.Tests.City
{
    public class CityModelTests
    {
        private CityModel _city;
        private BuildingModel _house;
        private BuildingModel _farm;
        private BuildingConfig _houseConfig;
        private BuildingConfig _farmConfig;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _houseConfig = AssetDatabase.LoadAssetAtPath<BuildingConfig>("Assets/Game/Configs/Buildings/House.asset");
            _farmConfig = AssetDatabase.LoadAssetAtPath<BuildingConfig>("Assets/Game/Configs/Buildings/Farm.asset");
        }

        [SetUp]
        public void SetUp()
        {
            _city = new CityModel();
            _house = new BuildingModel(_houseConfig);
            _farm = new BuildingModel(_farmConfig);
        }

        [Test]
        public void WhenAddBuildings_TheyAdded()
        {
            //Arrange
            var housePosition = new CityPosition(1, 1);
            var farmPosition = new CityPosition(2, 2);

            //Act
            _city.AddBuilding(_house, housePosition);
            _city.AddBuilding(_farm, farmPosition);

            //Assert
            _city.Buildings.Should().Contain(_house);
            _city.Buildings.Should().Contain(_farm);
            _city.BuildingsCount.Should().Be(2);
            _city.BuildingsToPositions.Should().Contain(new KeyValuePair<CityPosition, BuildingModel>(housePosition, _house));
            _city.BuildingsToPositions.Should().Contain(new KeyValuePair<CityPosition, BuildingModel>(farmPosition, _farm));
        }

        [Test]
        public void WhenAddBuilding_PositionInBuildingAreChange()
        {
            //Arrange
            var oldPosition = new CityPosition(2, 2);
            var newPosition = new CityPosition(1, 1);

            //Act
            _house.SetCityPosition(oldPosition);
            _city.AddBuilding(_house, newPosition);

            //Assert
            _house.Position.Should().Be(newPosition);
        }

        [Test]
        public void WhenAddBuildings_EventShouldBeInvoked()
        {
            //Arrange
            var invoked = false;
            var position = new CityPosition(2, 2);

            BuildingModel building = null;
            CityPosition buildingPosition = default;

            //Act
            _city.BuildingAdded += (pos, build) =>
            {
                invoked = true;
                building = build;
                buildingPosition = pos;
            };

            _city.AddBuilding(_house, position);

            //Arrange
            buildingPosition.Should().Be(position);
            building.Should().Be(_house);
            invoked.Should().BeTrue();
        }

        [Test]
        public void WhenRemoveBuilding_ItRemoved()
        {
            //Arrange
            var position = new CityPosition(2, 2);

            //Act
            _city.AddBuilding(_house, position);
            _city.RemoveBuilding(position);

            //Arrange
            _city.BuildingsCount.Should().Be(0);
            _city.Buildings.Should().NotContain(_house);
        }

        [Test]
        public void WhenRemoveBuildings_EventShouldBeInvoked()
        {
            //Arrange
            var invoked = false;
            var position = new CityPosition(2, 2);

            BuildingModel building = null;
            CityPosition buildingPosition = default;

            //Act
            _city.BuildingRemoved += (pos, build) =>
            {
                invoked = true;
                building = build;
                buildingPosition = pos;
            };

            _city.AddBuilding(_house, position);
            _city.RemoveBuilding(position);

            //Arrange
            buildingPosition.Should().Be(position);
            building.Should().Be(_house);
            invoked.Should().BeTrue();
        }

        [Test]
        public void WhenTryAddSameBuilding_ItShouldNotBeAdded()
        {
            //Arrange
            var position1 = new CityPosition(1, 1);
            var position2 = new CityPosition(2, 2);

            //Act
            _city.AddBuilding(_house, position1);
            _city.AddBuilding(_house, position2);

            //Assert
            _city.Buildings.Should().Contain(_house);
            _city.BuildingsCount.Should().Be(1);
            _house.Position.Should().Be(position1);
            _city.BuildingsToPositions[position1].Should().Be(_house);
        }

        [Test]
        public void WhenTryAddBuildingsToBusyPosition_ItIsNotAdded()
        {
            //Arrange
            var position = new CityPosition(1, 1);

            //Act
            _city.AddBuilding(_house, position);
            _city.AddBuilding(_farm, position);

            //Assert
            _city.Buildings.Should().Contain(_house);
            _city.BuildingsCount.Should().Be(1);
            _house.Position.Should().Be(position);
            _city.BuildingsToPositions[position].Should().Be(_house);
        }
    }
}