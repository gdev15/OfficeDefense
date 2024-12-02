using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TitleScreenController : MonoBehaviour
{
    private UnityEngine.UIElements.Button playButton;

    private void Start()
    {
        // Get the root VisualElement from the UIDocument
        var root = GetComponent<UIDocument>().rootVisualElement;

        if (root == null)
        {
            Debug.LogError("Root VisualElement is null. Check UIDocument setup.");
            return;
        }

        // Find the button by its name
        playButton = root.Q<UnityEngine.UIElements.Button>("playButton");

        if (playButton == null)
        {
            Debug.LogError("Play button not found. Check the button name in UXML.");
            return;
        }

        // Register the button's click event
        playButton.clicked += OnPlayButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("LevelOne");
    }
}
