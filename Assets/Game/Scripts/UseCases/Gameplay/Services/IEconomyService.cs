using R3;

namespace UseCases.Gameplay.Services
{
    public interface IEconomyService
    {
        public Observable<int> Money { get; }
    }
}