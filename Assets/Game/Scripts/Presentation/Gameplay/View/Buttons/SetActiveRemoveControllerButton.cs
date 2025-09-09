using System.Collections.Generic;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Presentation.Gameplay.View.Buttons
{
    public class SetActiveRemoveControllerButton : MonoBehaviour
    {
        [SerializeField] private string[] _buttonNames;
        [SerializeField] private bool _isActive;
        [SerializeField] private UIDocument _document;

        private IPublisher<SetActiveRemoveBuildingControllerDTO> _publisher;
        private readonly List<Button> _buttons = new();

        private void Awake()
        {
            foreach (var buttonName in _buttonNames)
                _buttons.Add(_document.rootVisualElement.Q<Button>(buttonName));
        }

        private void OnEnable()
        {
            foreach (var button in _buttons)
                button.RegisterCallback<ClickEvent>(OnButtonClicked);
        }

        private void OnDisable()
        {
            foreach (var button in _buttons)
                button.UnregisterCallback<ClickEvent>(OnButtonClicked);
        }

        [Inject]
        public void Construct(IPublisher<SetActiveRemoveBuildingControllerDTO> publisher)
        {
            _publisher = publisher;
        }

        private void OnButtonClicked(ClickEvent clickEvent)
        {
            _publisher.Publish(new SetActiveRemoveBuildingControllerDTO(_isActive));
        }
    }
}