using System;
using System.Collections.Generic;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using MessagePipe;
using ObservableCollections;
using Presentation.Gameplay.View;
using R3;
using UnityEngine;
using UseCases.Gameplay;
using UseCases.Gameplay.Cases;
using UseCases.Gameplay.Services;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class CreateBuildingsPresenter : IStartable, IDisposable
    {
        private readonly CityService _cityService;
        private readonly CreateBuildingUseCase _useCase;

        private readonly ISubscriber<CreateBuildingDTO> _subscriber;

        private readonly BuildingViewFactory _factory;

        private IDisposable _disposable;

        public CreateBuildingsPresenter(CityService cityService,
            CreateBuildingUseCase useCase,
            BuildingViewFactory factory,
            ISubscriber<CreateBuildingDTO> subscriber)
        {
            _cityService = cityService;
            _useCase = useCase;
            _factory = factory;

            _subscriber = subscriber;
        }

        public void Start()
        {
            var builder = Disposable.CreateBuilder();

            _cityService.Buildings.ObserveAdd().Subscribe(OnBuildingAdded).AddTo(ref builder);
            _cityService.Buildings.ObserveRemove().Subscribe(OnBuildingRemoved).AddTo(ref builder);
            _subscriber.Subscribe(_useCase.Handle).AddTo(ref builder);

            _disposable = builder.Build();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnBuildingAdded(CollectionAddEvent<KeyValuePair<Vector3, BuildingModel>> addEvent)
        {
            var position = addEvent.Value.Key;
            var building = addEvent.Value.Value;

            _factory.Create(building.Type, position);
        }

        private void OnBuildingRemoved(CollectionRemoveEvent<KeyValuePair<Vector3, BuildingModel>> removeEvent)
        {
            var position = removeEvent.Value.Key;
            var building = removeEvent.Value.Value;

            Debug.Log($"Необходимо убрать здание {building.Type} position: {position}");
        }
    }
}