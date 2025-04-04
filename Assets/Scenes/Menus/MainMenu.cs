using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button btnNewGame;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

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
    
    private void OnNewGameClicked()
    {
        Debug.Log("New Game Clicked!");
        // Implement your game logic here
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
