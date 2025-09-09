using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using R3;
using UseCases.Gameplay.Cases;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class RemoveBuildingPresenter : IStartable, IDisposable
    {
        private readonly RemoveBuildingUseCase _useCase;
        private readonly ISubscriber<RemoveBuildingDTO> _subscriber;

        private IDisposable _disposable;

        public RemoveBuildingPresenter(RemoveBuildingUseCase useCase, ISubscriber<RemoveBuildingDTO> subscriber)
        {
            _useCase = useCase;
            _subscriber = subscriber;
        }

        public void Start()
        {
            var builder = Disposable.CreateBuilder();

            _subscriber.Subscribe(_useCase.Handle).AddTo(ref builder);

            _disposable = builder.Build();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}