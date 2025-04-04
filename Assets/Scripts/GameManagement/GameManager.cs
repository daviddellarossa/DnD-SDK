using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = SceneManagement.SceneManager;

namespace GameManagement
{
    public class GameManager : SceneManager
    {
        public GameManagerCore Core { get; protected set; }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            OnStart();
        }

        private void OnStart()
        {
            Core.OnStart();
        }

        // Update is called once per frame
        void Awake()
        {
            OnAwake();
        }

        private void OnAwake()
        {
            Core = new GameManagerCore(this);
            Core.OnAwake();
        }
    }
}
