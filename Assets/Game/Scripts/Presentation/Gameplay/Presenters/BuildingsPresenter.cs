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
    public class CityPresenter : IStartable, IDisposable
    {
        private readonly ICityService _cityService;

        private readonly BuildingViewFactory _viewFactory;
        private readonly BuildingPresenterFactory _presenterFactory;

        private readonly Dictionary<BuildingService, BuildingView> _servicesToView = new();
        private readonly Dictionary<BuildingService, BuildingPresenter> _servicesToPresenters = new();

        private IDisposable _disposable;

        public CityPresenter(ICityService cityService,
            RemoveBuildingUseCase removeBuildingUseCase,
            BuildingViewFactory viewFactory,
            BuildingPresenterFactory presenterFactory,
            ISubscriber<RemoveBuildingDTO> removeBuildingSubscriber)
        {
            _cityService = cityService;
            _viewFactory = viewFactory;
            _presenterFactory = presenterFactory;
        }

        public void Start()
        {
            var builder = Disposable.CreateBuilder();

            _cityService.Buildings.ObserveAdd().Subscribe(OnBuildingAdded).AddTo(ref builder);
            _cityService.Buildings.ObserveRemove().Subscribe(OnBuildingRemoved).AddTo(ref builder);

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

        //TODO: Merge view/presenter and create pool system
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