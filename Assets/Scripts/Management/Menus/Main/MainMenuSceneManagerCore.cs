namespace Management.Menus.Main
{
    public class MainMenuSceneManagerCore
    {
        public MainMenuSceneManager Parent { get; set; }
        
        public MainMenuSceneManagerCore(MainMenuSceneManager parent)
        {
            Parent = parent;
        }
        
        public void StartGame()
        {
        }
        
        public void QuitGame()
        {
        }
        
        public void OnStart() { }

        public void OnAwake() { }

        public void OnEnable() { }

    }
}