using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int gameScene;

    [Header("Music Manager")]
    public MainMenuMusicManager musicManager;

    // Turns off the main menu music when the player presses play, and switches to the game scene.
    public void Play()
    {

        if (musicManager != null)
        {

            musicManager.StopMainMenuMusic();
            SceneManager.LoadScene(gameScene);

        }

    }

    // Player leaves the game.
    public void Quit()
    {

        Debug.Log("Player has quit the game");
        Application.Quit();

    }

}