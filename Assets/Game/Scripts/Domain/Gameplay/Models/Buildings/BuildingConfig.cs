using UnityEngine;

namespace Domain.Gameplay.Models.Buildings
{
    [CreateAssetMenu(fileName = "BuildingConfig", menuName = "Game/Configs/BuildingConfig")]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private BuildingType _type;
        [SerializeField] private int _cost = 25;
        [SerializeField] private int _income = 10;
        [SerializeField] private float _incomeDelay = 2f;

        public BuildingType Type => _type;
        public int Cost => _cost;
        public int Income => _income;
        public float IncomeDelay => _incomeDelay;
    }
}