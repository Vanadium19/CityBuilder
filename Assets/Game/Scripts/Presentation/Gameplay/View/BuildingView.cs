using System;
using Domain.Gameplay.Models.Buildings;
using UnityEngine;

namespace Presentation.Presentation.View
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private BuildingType _type;

        private Transform _transform;
        private Vector3 _startScale;

        public BuildingType Type => _type;

        private void Awake()
        {
            _transform = transform;
            _startScale = _transform.localScale;
        }

        public void UpdateLevel(int value)
        {
            var scale = _startScale;
            _startScale.x *= value;
            _startScale.z *= value;

            transform.localScale = scale;
        }
    }
}