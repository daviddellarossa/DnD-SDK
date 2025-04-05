using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management.Game
{
    public delegate void SceneLoadedEventArgs(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode);
    public delegate void SceneUnloadedEventArgs(UnityEngine.SceneManagement.Scene scene);
    
    public class SceneManagerWrapper
    {
        public event SceneLoadedEventArgs SceneLoaded;
        public event SceneUnloadedEventArgs SceneUnloaded;
        
        private static SceneManagerWrapper _instance;
        
        public static SceneManagerWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneManagerWrapper();
                }
                return _instance;
            }
        }
        
        private SceneManagerWrapper()
        {
            SceneManager.sceneLoaded += (scene, mode) => SceneLoaded?.Invoke(scene, mode);
            SceneManager.sceneUnloaded += scene => SceneUnloaded?.Invoke(scene);
        }
        
        public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public AsyncOperation UnloadSceneAsync(string sceneName)
        {
            return SceneManager.UnloadSceneAsync(sceneName);
        }

    }
}