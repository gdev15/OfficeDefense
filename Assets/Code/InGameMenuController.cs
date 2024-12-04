using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameMenuController : MonoBehaviour
{
    private VisualElement root; // Root of the UIDocument
    private Button resumeButton;
    private Button restartButton;
    private Button quitButton;
    private bool isPaused = false;

    private UIDocument uiDocument;

    void Start()
    {
        // Get reference to the UIDocument and its root
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        // Assign buttons from the UI
        resumeButton = root.Q<Button>("ResumeButton");
        restartButton = root.Q<Button>("RestartButton");
        quitButton = root.Q<Button>("QuitButton");

        // Hook up button click events
        resumeButton.clicked += ResumeGame;
        restartButton.clicked += RestartLevel;
        quitButton.clicked += QuitToMainMenu;

        // Ensure the UI starts hidden
        root.style.display = DisplayStyle.None;
    }

    void Update()
    {
        // Toggle pause menu with the 'Esc' key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pause the game
    public void PauseGame()
    {
        root.style.display = DisplayStyle.Flex; // Show the UI
        Time.timeScale = 0f; // Stop the game time
        isPaused = true;
    }

    // Resume the game
    public void ResumeGame()
    {
        root.style.display = DisplayStyle.None; // Hide the UI
        Time.timeScale = 1f; // Resume the game time
        isPaused = false;   
    }

    // Restart the current level
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Ensure game time resumes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit to the main menu
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Ensure game time resumes
        SceneManager.LoadScene("TitleScreen"); // Replace with your actual main menu scene name
    }
}
