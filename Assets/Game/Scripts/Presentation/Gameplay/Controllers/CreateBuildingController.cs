using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using MessagePipe;
using Presentation.Gameplay.View;
using UnityEngine;
using VContainer.Unity;

namespace Presentation.Gameplay.Controllers
{
    public class CreateBuildingController : ITickable, ICreateBuildingController
    {
        private readonly Camera _camera;
        private readonly IPublisher<CreateBuildingDTO> _subscriber;

        private BuildingConfig _config;

        private bool _isActive;

        public CreateBuildingController(Camera camera, IPublisher<CreateBuildingDTO> subscriber)
        {
            _camera = camera;
            _subscriber = subscriber;
        }

        public void Tick()
        {
            if (!_isActive)
                return;

            if (!_config)
                return;

            if (!Input.GetKeyDown(KeyCode.Mouse0))
                return;

            CreateBuilding();
        }

        public void SetBuilding(BuildingConfig config)
        {
            _config = config;
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

        private void CreateBuilding()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out CityView view))
                _subscriber.Publish(new CreateBuildingDTO(_config, hit.point));
        }
    }
}