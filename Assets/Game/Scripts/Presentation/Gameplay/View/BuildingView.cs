using Domain.Gameplay.Models.Buildings;
using UnityEngine;

namespace Presentation.Presentation.View
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private BuildingType _type;

        public BuildingType Type => _type;
    }
}