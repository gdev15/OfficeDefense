using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public GameObject deathMenu;   // Menu for restarting game after death
    // Can add more menu gameobjects here

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Hide();
    }

    public void Show() // Show the main menu
    {
        ShowMainMenu(); // For when we have a main menu
        gameObject.SetActive(true);
        Time.timeScale = 0;
        PlayerMovement.instance.isPaused = true;
    }

    public void Hide() // Hide all of the menus
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if(PlayerMovement.instance != null)
        {
            PlayerMovement.instance.isPaused = false;
        }
    }

    void SwitchMenu(GameObject someMenu) // Switch menus with multiple menus
    {
        // Turn off all other menus
        
        // Turn on the requested menu
        someMenu.SetActive(true);
    }

    public void ShowMainMenu() { } // Do this for all menus with SwitchMenu(name-of-menu);

    public void ShowDeathMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        SwitchMenu(deathMenu);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
