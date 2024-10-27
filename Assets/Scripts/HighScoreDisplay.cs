using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    private HighScoreManager highScoreManager;

    // Define TextMeshPro fields for each score and nickname.
    public TMP_Text[] scoreTexts; // Array for the score boxes.
    public TMP_Text[] nameTexts; // Array for the nickname boxes.

    private void Awake()
    {
        highScoreManager = FindObjectOfType<HighScoreManager>();

        // Gives a warning if no highScoreManager is found.
        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager not found in the scene!");
        }
        else
        {
            highScoreManager.LoadScores(); // Make sure the scores are loaded at startup.
        }
    }

    void Start()
    {
        DisplayHighScores(); // Load highscore and display.
    }

    public void DisplayHighScores()
    {
        // Checks if the requirements exist for the rest to work.
        if (highScoreManager == null || highScoreManager.highScores == null)
        {
            Debug.LogWarning("HighScoreManager or highScores list is null.");
            return;
        }

        if (scoreTexts == null || nameTexts == null)
        {
            Debug.LogWarning("The ScoreTexts or NameTexts arrays are not assigned.");
            return;
        }

        highScoreManager.LoadScores();

        // Loads scores and nicknames.
        for (int i = 0; i < highScoreManager.highScores.Count && i < scoreTexts.Length; i++)
        {
            if (scoreTexts[i] != null && nameTexts[i] != null)
            {
                scoreTexts[i].text = highScoreManager.highScores[i].Score.ToString("00000");
                nameTexts[i].text = highScoreManager.highScores[i].Nickname;
            }
            else
            {
                Debug.LogWarning($"The text element at index { i} is null.");
    }
}
    }
}