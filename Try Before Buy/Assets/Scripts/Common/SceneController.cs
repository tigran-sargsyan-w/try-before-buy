using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Common
{
    public class SceneController
    {
        private readonly List<string> scenes = new List<string>();
        
        private static SceneController instance;

        public static SceneController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneController();
                }
                return instance;
            }
        }
        private SceneController()
        {
            
        }
        public List<string> Scenes => scenes;

        public void Init()
        {
            GetAllScenes();
        }
        
        private void GetAllScenes()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            scenes.Clear();
            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                scenes.Add(sceneName);
            }
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}