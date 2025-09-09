using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UseCases.Gameplay.Cases;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class UpgradeBuildingsPresenter : IStartable, IDisposable
    {
        private readonly UpgradeBuildingUseCase _useCase;
        private readonly ISubscriber<UpgradeBuildingDTO> _subscriber;

        private IDisposable _disposable;

        public UpgradeBuildingsPresenter(UpgradeBuildingUseCase useCase, ISubscriber<UpgradeBuildingDTO> subscriber)
        {
            _useCase = useCase;
            _subscriber = subscriber;
        }

        public void Start()
        {
            var bag = DisposableBag.CreateBuilder();

            _subscriber.Subscribe(_useCase.Handle).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}