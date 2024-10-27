using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private HighScoreManager highScoreManager;
    public GameObject highScoreInputUI;
    public TMP_InputField nicknameInputField;
    public int mainMenu;

    private void Awake()
    {
        highScoreManager = FindObjectOfType<HighScoreManager>();

        // Gives a warning if no highScoreManager is found.
        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager not found in the scene!");
        }
    }

    // This method is called when the game ends (in case of player death or victory).
    public void GameOver(int score)
    {
        int playerScore = GameManager.Instance.score;

        // Check if the player's score is high enough to be added to the highest score list.
        if (highScoreManager.IsHighScore(score))
        {
            highScoreInputUI.SetActive(true);
            nicknameInputField.text = "";
        }
        else
        {
            ReturnToMainMenu(); // Go straight back to the main menu if you do not get a high score.
        }
    }

    // Method to submit the nickname and save the score.
    public void SubmitScore()
    {
        string nickname = nicknameInputField.text;

        // Ensure that the player's nickname is valid (not empty and exactly 3 characters long).
        if (!string.IsNullOrEmpty(nickname) && nickname.Length == 3)
        {
            highScoreManager.AddScore(GameManager.Instance.score, nickname);
            highScoreInputUI.SetActive(false);
            ReturnToMainMenu();
        }
        else
        {
            Debug.LogWarning("Nickname must be exactly 3 characters long."); // If time available: Add so feedback for incorrect input is given to the player.
        }
    }

    private void Update()
    {
        // Check for "Enter" key only if high score input UI is active
        if (Input.GetKeyDown(KeyCode.Return) && highScoreInputUI.activeSelf)
        {
            SubmitScore();  // Only submit score if UI is active
        }
    }

    // This method handles the return to the main menu.
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}