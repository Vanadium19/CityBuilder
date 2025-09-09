using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Presentation.View;
using UnityEngine;
using VContainer.Unity;

namespace Presentation.Gameplay.Controllers
{
    public class UpgradeBuildingController : ITickable
    {
        private readonly Camera _camera;
        private readonly IPublisher<UpgradeBuildingDTO> _publisher;

        private bool _isActive;

        public UpgradeBuildingController(Camera camera, IPublisher<UpgradeBuildingDTO> publisher)
        {
            _camera = camera;
            _publisher = publisher;
        }

        public void Tick()
        {
            if (!_isActive)
                return;

            if (!Input.GetKeyDown(KeyCode.Mouse0))
                return;

            RemoveBuilding();
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

        private void RemoveBuilding()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out BuildingView view))
                _publisher.Publish(new UpgradeBuildingDTO(hit.transform.position));
        }
    }
}