using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using MessagePipe;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Presentation.Gameplay.View.Buttons
{
    public class SetConfigInControllerButton : MonoBehaviour
    {
        [SerializeField] private string _buttonName;
        [SerializeField] private BuildingConfig _config;
        [SerializeField] private UIDocument _document;

        private IPublisher<SetConfigInBuilderControllerDTO> _publisher;
        private Button _button;

        private void Awake()
        {
            _button = _document.rootVisualElement.Q<Button>(_buttonName);
        }

        private void OnEnable()
        {
            _button.RegisterCallback<ClickEvent>(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.UnregisterCallback<ClickEvent>(OnButtonClicked);
        }

        [Inject]
        public void Construct(IPublisher<SetConfigInBuilderControllerDTO> publisher)
        {
            _publisher = publisher;
        }

        private void OnButtonClicked(ClickEvent clickEvent)
        {
            _publisher.Publish(new SetConfigInBuilderControllerDTO(_config));
        }
    }
}