using UnityEngine;

namespace _Project._Code.Features.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Show()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1;
        }
        
        public void Hide()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0;
        }
    }
}