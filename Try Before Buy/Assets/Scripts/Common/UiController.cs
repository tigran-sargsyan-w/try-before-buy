using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class UiController : MonoBehaviour
    {
        public Button tryOnGlassesButton;
        public Button tryOnWatchesButton;
        private SceneController sceneController;

        
        public void Init()
        {
            sceneController = SceneController.Instance;
            SubscribeButtonsEvents();
        }

        private void SubscribeButtonsEvents()
        {
            tryOnGlassesButton.onClick.AddListener(OnTryOnGlassesButtonClick);
            tryOnWatchesButton.onClick.AddListener(OnTryOnWatchesButtonClick);
        }
        
        private void UnsubscribeButtonsEvents()
        {
            tryOnGlassesButton.onClick.RemoveAllListeners();
            tryOnWatchesButton.onClick.RemoveAllListeners();
        }

        private void OnTryOnGlassesButtonClick()
        {
            sceneController.LoadScene(sceneController.Scenes[1]);
            UnsubscribeButtonsEvents();
        }

        private void OnTryOnWatchesButtonClick()
        {
            sceneController.LoadScene(sceneController.Scenes[2]);
            UnsubscribeButtonsEvents();
        }
    }
}