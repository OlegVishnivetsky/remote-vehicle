using System;
using System.Collections.Generic;
using _Project._Code.Infrastructure.StateMachine;
using _Project._Code.Infrastructure.States;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project._Code.Features.MapLoader.View
{
    public class MapLoaderView : MonoBehaviour
    {
        [SerializeField] private MapLoaderButton _loaderButtonPrefab;
        [SerializeField] private Transform _loaderButtonsContainer;

        private IGameStateMachine _gameStateMachine;
        
        private readonly List<MapLoaderButton> _loaderButtons = new();
        
        [Inject]
        public void Construct(IGameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;
        
        private void Start() => SpawnButtons();

        private void OnDestroy() => Cleanup();

        private void SpawnButtons()
        {
            for (int i = 1; i < (int)MapTypeId.Count; i++)
            {
                MapLoaderButton button = Instantiate(_loaderButtonPrefab, _loaderButtonsContainer);
                button.Construct((MapTypeId)i);
                button.Clicked += OnClicked;
                _loaderButtons.Add(button);
            }
            
            EventSystem.current.SetSelectedGameObject(_loaderButtons[0].gameObject);
        }

        private void Cleanup()
        {
            foreach (MapLoaderButton button in _loaderButtons)
            {
                button.Clicked -= OnClicked;
                Destroy(button.gameObject);
            }
            
            _loaderButtons.Clear();
        }

        private void OnClicked(MapTypeId mapToLoad) => _gameStateMachine.SwitchTo<LoadMapState, MapTypeId>(mapToLoad);
    }
}