using System;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using MessagePipe;
using Presentation.Gameplay.View;
using UnityEngine;
using VContainer.Unity;

namespace Presentation.Gameplay.Controllers
{
    public class CreateBuildingController : IInitializable, ITickable, IDisposable
    {
        private readonly Camera _camera;
        private readonly IPublisher<CreateBuildingDTO> _publisher;

        private readonly ISubscriber<SetActiveCreateBuildingControllerDTO> _setActiveSubscriber;
        private readonly ISubscriber<SetConfigInBuilderControllerDTO> _setConfigSubscriber;

        private IDisposable _disposable;

        private BuildingConfig _config;
        private bool _isActive;

        public CreateBuildingController(Camera camera,
            IPublisher<CreateBuildingDTO> publisher,
            ISubscriber<SetConfigInBuilderControllerDTO> setConfigSubscriber,
            ISubscriber<SetActiveCreateBuildingControllerDTO> setActiveSubscriber)
        {
            _camera = camera;
            _publisher = publisher;

            _setActiveSubscriber = setActiveSubscriber;
            _setConfigSubscriber = setConfigSubscriber;
        }

        public void Initialize()
        {
            var bag = DisposableBag.CreateBuilder();

            _setActiveSubscriber.Subscribe(SetActive).AddTo(bag);
            _setConfigSubscriber.Subscribe(SetBuilding).AddTo(bag);

            _disposable = bag.Build();
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

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void SetBuilding(SetConfigInBuilderControllerDTO dto)
        {
            _config = dto.Config;
        }

        private void SetActive(SetActiveCreateBuildingControllerDTO dto)
        {
            _isActive = dto.IsActive;
        }

        private void CreateBuilding()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out CityView view))
                _publisher.Publish(new CreateBuildingDTO(_config, hit.point));
        }
    }
}