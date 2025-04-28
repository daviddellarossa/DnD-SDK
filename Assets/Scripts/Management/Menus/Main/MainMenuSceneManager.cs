using Management.Game;
using UnityEngine;
using UnityEngine.UIElements;

namespace Management.Menus.Main
{
    public class MainMenuSceneManager : MenuManager
    {
        public MainMenuSceneManagerCore Core { get; protected set; }
        
        private UIDocument uiDocument;
        private Button btnNewGame;
        private Button btnContinueGame;
        private Button btnQuitGame;

        void Awake()
        {
            Core = new MainMenuSceneManagerCore(this);

            Core.OnAwake();
            
            uiDocument = gameObject.GetComponent<UIDocument>();

            if (uiDocument != null)
            {
                var root = uiDocument.rootVisualElement;

                // Get a reference to the button by name (from UXML)
                btnNewGame = root.Q<Button>("btnNewGame"); // Must match the name in UXML

                if (btnNewGame != null)
                {
                    btnNewGame.clicked += OnNewGameClicked;
                }
                
                // Get a reference to the button by name (from UXML)
                btnContinueGame = root.Q<Button>("btnContinueGame"); // Must match the name in UXML

                if (btnContinueGame != null)
                {
                    btnContinueGame.clicked += OnContinueGameClicked;
                }
                
                // Get a reference to the button by name (from UXML)
                btnQuitGame = root.Q<Button>("btnQuitGame"); // Must match the name in UXML

                if (btnQuitGame != null)
                {
                    btnQuitGame.clicked += OnQuitGameClicked;
                }
            }
        }

        private void OnQuitGameClicked()
        {
            DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.Send_OnQuitGame(this, nameof(GameManager));
        }

        void Start()
        {
            Core.OnStart();
        }
        
        public void StartGame()
        {
            Core.StartGame();
        }
        
        private void OnNewGameClicked()
        {
            Debug.Log("New Game Clicked!");
            // Implement your game logic here
            DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.Send_OnStartGame(this, nameof(GameManager));
        }

        private void OnContinueGameClicked()
        {
            Debug.Log("Continue Game Clicked!");
            // Implement your game logic here
            DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.Send_OnLoadLatestGame(this, null);
        }
    }
}