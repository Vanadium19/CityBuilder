using System;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Grid;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;

namespace Domain.Gameplay.Tests.Buildings
{
    public class BuildingModelTests
    {
        private BuildingConfig _config;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _config = AssetDatabase.LoadAssetAtPath<BuildingConfig>("Assets/Game/Configs/Buildings/House.asset");
        }

        [Test]
        public void WhenInitWithNullConfig_ArgumentNullException()
        {
            //Act
            Action act = () => new BuildingModel(null);

            //Assert
            act.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null.\nParameter name: config");
        }

        [Test]
        public void WhenInitWithConfig_FieldsAreSet()
        {
            //Arrange
            var type = _config.Type;

            //Act
            var building = new BuildingModel(_config);

            //Assert
            building.Type.Should().Be(type);
            building.Level.Should().Be(BuildingModel.StartLevel);
        }

        [Test]
        public void WhenSetGridPosition_ItAreSet()
        {
            //Arrange
            var building = new BuildingModel(_config);
            var position = new GridPosition(0, 1);

            //Act
            building.SetGridPosition(position);

            //Arrange
            building.Position.Should().Be(position);
        }

        [Test]
        public void WhenBuildingUpgraded_LevelChanged()
        {
            //Arrange
            var building = new BuildingModel(_config);

            //Act
            building.Upgrade();

            //Assert
            building.Level.Should().Be(2);
        }

        [Test]
        public void WhenBuildingUpgrade_EventShouldBeInvoked()
        {
            //Arrange
            var level = 0;
            var invoked = false;
            var building = new BuildingModel(_config);

            //Act
            building.LevelUpgraded += (l) =>
            {
                invoked = true;
                level = l;
            };
            building.Upgrade();

            //Assert
            building.Level.Should().Be(2);
            invoked.Should().BeTrue();
            level.Should().Be(2);
        }

        [Test]
        public void WhenUpgradeToMaximum_AfterUpgrade_Nothing()
        {
            //Arrange
            bool invoked = false;

            var minLevel = BuildingModel.StartLevel;
            var maxLevel = BuildingModel.MaxLevel;
            var building = new BuildingModel(_config);


            //Act
            for (int i = minLevel; i < maxLevel; i++)
                building.Upgrade();

            building.LevelUpgraded += _ => invoked = true;
            building.Upgrade();

            //Assert
            building.CanUpgrade.Should().BeFalse();
            building.Level.Should().Be(maxLevel);
            invoked.Should().BeFalse();
        }
    }
}