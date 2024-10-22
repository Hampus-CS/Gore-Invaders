using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public HighScores highScores;
    public GameObject nameInputPanel; 
    public GameObject mainMenuButton;

    // TMP_InputField för nameInputPanel

    private int playerScore; // Temporär tills sammankopplingen med GameManager har gjorts.

    public int mainMenu;

    public void OnPlayerDeath(int score)
    {
        playerScore = score;

        if (highScores.IsNewHighScore(playerScore))
        {
            nameInputPanel.SetActive(true);
            mainMenuButton.SetActive(false);
        }
        else
        {
            nameInputPanel.SetActive(false);
            mainMenuButton.SetActive(true);
        }
    }

    // Anropar denna metod när spelaren skickar in sitt namn
    public void SubmitName(string playerName)
    {
        highScores.AddNewScore(playerName, playerScore);
        // Visa huvudmenyknappen igen efter att du har skickat in namnet
        mainMenuButton.SetActive(true);
    }

    // Anropar denna metod när spelaren väljer att gå till huvudmenyn
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
