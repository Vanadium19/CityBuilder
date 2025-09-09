using System;
using System.Collections.Generic;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using ObservableCollections;
using Presentation.Gameplay.View;
using Presentation.Presentation.View;
using R3;
using UnityEngine;
using UseCases.Gameplay.Cases;
using UseCases.Gameplay.Services;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Presentation.Gameplay.Presenters
{
    //TODO: Поделить на два презентера
    public class BuildingsPresenter : IStartable, IDisposable
    {
        private readonly CityService _cityService;

        private readonly BuildingViewFactory _viewFactory;
        private readonly BuildingPresenterFactory _presenterFactory;

        private readonly CreateBuildingUseCase _createBuildingUseCase;
        private readonly RemoveBuildingUseCase _removeBuildingUseCase;

        private readonly ISubscriber<CreateBuildingDTO> _createBuildingSubscriber;
        private readonly ISubscriber<RemoveBuildingDTO> _removeBuildingSubscriber;

        private readonly Dictionary<BuildingService, BuildingView> _servicesToView = new();
        private readonly Dictionary<BuildingService, BuildingPresenter> _servicesToPresenters = new();

        private IDisposable _disposable;

        public BuildingsPresenter(CityService cityService,
            CreateBuildingUseCase createBuildingUseCase,
            RemoveBuildingUseCase removeBuildingUseCase,
            BuildingViewFactory viewFactory,
            BuildingPresenterFactory presenterFactory,
            ISubscriber<CreateBuildingDTO> createBuildingSubscriber,
            ISubscriber<RemoveBuildingDTO> removeBuildingSubscriber)
        {
            _cityService = cityService;

            _viewFactory = viewFactory;
            _presenterFactory = presenterFactory;

            _createBuildingUseCase = createBuildingUseCase;
            _removeBuildingUseCase = removeBuildingUseCase;

            _createBuildingSubscriber = createBuildingSubscriber;
            _removeBuildingSubscriber = removeBuildingSubscriber;
        }

        public void Start()
        {
            var builder = Disposable.CreateBuilder();

            _cityService.Buildings.ObserveAdd().Subscribe(OnBuildingAdded).AddTo(ref builder);
            _cityService.Buildings.ObserveRemove().Subscribe(OnBuildingRemoved).AddTo(ref builder);
            _createBuildingSubscriber.Subscribe(_createBuildingUseCase.Handle).AddTo(ref builder);
            _removeBuildingSubscriber.Subscribe(_removeBuildingUseCase.Handle).AddTo(ref builder);

            _disposable = builder.Build();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnBuildingAdded(CollectionAddEvent<KeyValuePair<Vector3, BuildingService>> addEvent)
        {
            var position = addEvent.Value.Key;
            var building = addEvent.Value.Value;

            var view = _viewFactory.Create(building.Type, position);
            var presenter = _presenterFactory.Create(building, view);
            _servicesToView.Add(building, view);
            _servicesToPresenters.Add(building, presenter);
        }

        private void OnBuildingRemoved(CollectionRemoveEvent<KeyValuePair<Vector3, BuildingService>> removeEvent)
        {
            var building = removeEvent.Value.Value;

            if (_servicesToView.Remove(building, out BuildingView view))
                Object.Destroy(view.gameObject);

            if (_servicesToPresenters.Remove(building, out BuildingPresenter presenter))
                presenter.Dispose();
        }
    }
}