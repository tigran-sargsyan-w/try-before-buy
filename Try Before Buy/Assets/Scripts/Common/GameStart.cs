using UnityEngine;

namespace Common
{
    public class GameStart : MonoBehaviour
    {
        public UiController uiController;
        private SceneController sceneController;
      

        private void Start()
        {
            InitControllers();
        }

        private void InitControllers()
        {
            sceneController = SceneController.Instance;
            sceneController.Init();
            
            uiController.Init();
        }
    }
}