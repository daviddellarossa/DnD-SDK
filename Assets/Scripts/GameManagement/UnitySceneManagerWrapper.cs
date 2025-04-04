using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public delegate void SceneLoadedEventArgs(Scene scene, LoadSceneMode loadSceneMode);
    public delegate void SceneUnloadedEventArgs(Scene scene);
    
    public class UnitySceneManagerWrapper
    {
        public event SceneLoadedEventArgs SceneLoaded;
        public event SceneUnloadedEventArgs SceneUnloaded;
        
        private static UnitySceneManagerWrapper _instance;
        
        public static UnitySceneManagerWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnitySceneManagerWrapper();
                }
                return _instance;
            }
        }
        
        private UnitySceneManagerWrapper()
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