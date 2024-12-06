using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TitleScreenController : MonoBehaviour
{
    private TextField usernameField;
    private UnityEngine.UIElements.Button playButton;
    private const int maxUsernameLength = 12;


    private void Start()
    {
        // Get the root VisualElement from the UIDocument
        var root = GetComponent<UIDocument>().rootVisualElement;

        if (root == null)
        {
            Debug.LogError("Root VisualElement is null. Check UIDocument setup.");
            return;
        }

        // Find the button by its name "playButton" and the TextField by its name "TextFieldPlayersName"
        usernameField = root.Q<TextField>("TextFieldPlayersName");
        playButton = root.Q<UnityEngine.UIElements.Button>("playButton");

        if (usernameField == null || playButton == null)
        {
            Debug.LogError("UI elements not found in UXML.");
            return;
        }

        // Add event listener to validate username input

        usernameField.RegisterValueChangedCallback(evt =>
        {
            string currentText = evt.newValue;

            // Truncate the text if it exceeds the maximum length
            if (currentText.Length > maxUsernameLength)
            {
                usernameField.value = currentText.Substring(0, maxUsernameLength);
            }
        });

        // Register the button's click event
        playButton.clicked += OnPlayButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        string username = usernameField.value;

        // Validate username (optional, e.g., ensure it's not empty)
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogWarning("Username cannot be empty.");
            return;
        }

        // Save the username to PlayerPrefs for persistence
        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.Save();

        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelOne");
    }
}
