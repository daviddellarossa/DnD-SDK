using Scene_SceneManager = Management.Scene.SceneManager;

namespace Management.Game
{
    public class GameManager : Scene_SceneManager
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
