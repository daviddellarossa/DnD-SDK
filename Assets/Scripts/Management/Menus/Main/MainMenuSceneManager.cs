﻿using Management.Game;
using UnityEngine;
using UnityEngine.UIElements;

namespace Management.Menus.Main
{
    public class MainMenuSceneManager : MenuManager
    {
        public MainMenuSceneManagerCore Core { get; protected set; }
        
        private UIDocument uiDocument;
        private Button btnNewGame;
        
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
            }
        }
        
        void Start()
        {
            Core.OnStart();
        }
        
        public void StartGame()
        {
            Core.StartGame();
        }
        
        public void QuitGame()
        { 
            Core.QuitGame();
        }
        
        private void OnNewGameClicked()
        {
            Debug.Log("New Game Clicked!");
            // Implement your game logic here
            DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.Send_StartGame(this, nameof(GameManager));
        }

    }
}