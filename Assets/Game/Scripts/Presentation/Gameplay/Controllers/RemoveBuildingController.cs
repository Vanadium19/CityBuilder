using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Gameplay.View;
using Presentation.Presentation.View;
using UnityEngine;
using VContainer.Unity;

namespace Presentation.Gameplay.Controllers
{
    public class RemoveBuildingController : ITickable //IInitializable, IDisposable
    {
        private readonly Camera _camera;
        private readonly IPublisher<RemoveBuildingDTO> _publisher;

        private bool _isActive;

        public RemoveBuildingController(Camera camera, IPublisher<RemoveBuildingDTO> publisher)
        {
            _camera = camera;
            _publisher = publisher;
        }

        // public void Initialize()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public void Dispose()
        // {
        //     throw new NotImplementedException();
        // }

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
                _publisher.Publish(new RemoveBuildingDTO(hit.transform.position));
        }
    }
}