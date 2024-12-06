using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaderBoardController : MonoBehaviour
{
    private VisualElement root;
    private UIDocument uiDocument;

    private Label leaderBoardUsernameOne;
    private Label leaderBoardUsernameTwo;
    private Label leaderBoardUsernameThree;
    private Label leaderBoardUsernameFour;

    private Label leaderBoardScoreOne;
    private Label leaderBoardScoreTwo;
    private Label leaderBoardScoreThree;
    private Label leaderBoardScoreFour;

    private List<(string username, int score)> leaderboard;

    private void Start()
    {
        // Get reference to the UIDocument and its root
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        // Assign labels
        leaderBoardUsernameOne = root.Q<Label>("LeaderBoardUsernameOne");
        leaderBoardUsernameTwo = root.Q<Label>("LeaderBoardUsernameTwo");
        leaderBoardUsernameThree = root.Q<Label>("LeaderBoardUsernameThree");
        leaderBoardUsernameFour = root.Q<Label>("LeaderBoardUsernameFour");

        leaderBoardScoreOne = root.Q<Label>("LeaderBoardScoreOne");
        leaderBoardScoreTwo = root.Q<Label>("LeaderBoardScoreTwo");
        leaderBoardScoreThree = root.Q<Label>("LeaderBoardScoreThree");
        leaderBoardScoreFour = root.Q<Label>("LeaderBoardScoreFour");

        // Initialize leaderboard with random players and actual player
        InitializeLeaderboard();

        // Start updating the leaderboard dynamically
        StartCoroutine(UpdateLeaderboardRoutine());
    }

    private void InitializeLeaderboard()
    {
        leaderboard = new List<(string username, int score)>();

        // Generate random scores and levels for the first three slots
        for (int i = 1; i <= 3; i++)
        {
            string randomName = $"Player{i}";
            int randomScore = Random.Range(100, 1000);
            leaderboard.Add((randomName, randomScore));
        }

        // Add actual player to the 4th slot
        // Fetch the username the user entered on the title screen
        string playerName = PlayerPrefs.HasKey("Username") ? PlayerPrefs.GetString("Username") : "Player";

        leaderboard.Add((playerName, 0));

        // Sort the leaderboard by score (descending)
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

        // Display the leaderboard
        UpdateLeaderboardUI();
    }

    private IEnumerator UpdateLeaderboardRoutine()
    {
        while (true)
        {
            // Fetch the player's username from PlayerPrefs
            string playerName = PlayerPrefs.GetString("Username", "Player");

            // Update the actual player's score in the leaderboard
            leaderboard.RemoveAll(entry => entry.username == playerName);
            leaderboard.Add((playerName, GameController.instance.score));

            // Sort the leaderboard by score (descending)
            leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

            // Update the UI
            UpdateLeaderboardUI();

            // Wait before updating again
            yield return new WaitForSeconds(1f);
        }
    }


    private void UpdateLeaderboardUI()
    {
        // Update each slot with leaderboard data
        if (leaderboard.Count > 0)
        {
            leaderBoardUsernameOne.text = leaderboard[0].username;
            leaderBoardScoreOne.text = leaderboard[0].score.ToString();
        }

        if (leaderboard.Count > 1)
        {
            leaderBoardUsernameTwo.text = leaderboard[1].username;
            leaderBoardScoreTwo.text = leaderboard[1].score.ToString();
        }

        if (leaderboard.Count > 2)
        {
            leaderBoardUsernameThree.text = leaderboard[2].username;
            leaderBoardScoreThree.text = leaderboard[2].score.ToString();
        }

        if (leaderboard.Count > 3)
        {
            leaderBoardUsernameFour.text = leaderboard[3].username;
            leaderBoardScoreFour.text = leaderboard[3].score.ToString();
        }
    }
}
