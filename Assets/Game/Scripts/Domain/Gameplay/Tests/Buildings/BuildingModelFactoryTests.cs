using System;
using Domain.Gameplay.Models.Buildings;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;

namespace Domain.Gameplay.Tests.Buildings
{
    public class BuildingModelFactoryTests
    {
        private BuildingConfig _config;
        private BuildingModelFactory _factory;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _config = AssetDatabase.LoadAssetAtPath<BuildingConfig>("Assets/Game/Configs/Buildings/House.asset");
            _factory = new BuildingModelFactory();
        }

        [Test]
        public void WhenCreateWithNullConfig_ArgumentNullException()
        {
            //Act
            Action act = () => _factory.Create(null);

            //Assert
            act.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null.\nParameter name: config");
        }

        [Test]
        public void WhenCreate_CreatedRightBuilding()
        {
            //Arrange
            var type = _config.Type;

            //Act
            var building = _factory.Create(_config);

            //Assert
            building.Type.Should().Be(type);
        }
    }
}