using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project._Code.Features.MapLoader.View
{
    public class MapLoaderButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private MapTypeId _mapToLoad;
        
        public event Action<MapTypeId> Clicked;

        public void Construct(MapTypeId mapToLoad)
        {
            _mapToLoad = mapToLoad;
            _text.text = mapToLoad.ToString();
        }

        private void Start() => _button.onClick.AddListener(InvokeClick);

        private void OnDestroy() => _button.onClick.RemoveListener(InvokeClick);
        
        private void InvokeClick() => Clicked?.Invoke(_mapToLoad);
    }
}