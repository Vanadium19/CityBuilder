using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Gameplay.View;
using Presentation.Presentation.View;
using UnityEngine;
using VContainer.Unity;

namespace Presentation.Gameplay.Controllers
{
    public class RemoveBuildingController : IInitializable, ITickable, IDisposable
    {
        private readonly Camera _camera;
        private readonly IPublisher<RemoveBuildingDTO> _publisher;
        private readonly ISubscriber<SetActiveRemoveBuildingControllerDTO> _subscriber;

        private IDisposable _disposable;

        private bool _isActive;

        public RemoveBuildingController(Camera camera,
            IPublisher<RemoveBuildingDTO> publisher,
            ISubscriber<SetActiveRemoveBuildingControllerDTO> subscriber)
        {
            _camera = camera;
            _publisher = publisher;
            _subscriber = subscriber;
        }

        public void Initialize()
        {
            var bag = DisposableBag.CreateBuilder();

            _subscriber.Subscribe(SetActive).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Tick()
        {
            if (!_isActive)
                return;

            if (!Input.GetKeyDown(KeyCode.Mouse0))
                return;

            RemoveBuilding();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void SetActive(SetActiveRemoveBuildingControllerDTO dto)
        {
            _isActive = dto.IsActive;
        }

        private void RemoveBuilding()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out BuildingView view))
                _publisher.Publish(new RemoveBuildingDTO(hit.transform.position));
        }
    }
}