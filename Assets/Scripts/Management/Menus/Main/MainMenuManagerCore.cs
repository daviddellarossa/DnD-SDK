namespace Management.Menus.Main
{
    public class MainMenuManagerCore
    {
        public MainMenuManager Parent { get; set; }
        
        public MainMenuManagerCore(MainMenuManager parent)
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