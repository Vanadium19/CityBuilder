using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Gameplay.View
{
    public class MoneyView : MonoBehaviour
    {
        private const string Name = "Money";

        [SerializeField] private UIDocument _document;

        private Label _label;

        private void Awake()
        {
            _label = _document.rootVisualElement.Q<Label>(Name);
        }

        public void SetMoney(string value)
        {
            _label.text = value;
        }
    }
}