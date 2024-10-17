using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int gameScene;

    [Header("Music Manager")]
    public MainMenuMusicManager musicManager;
    
    public void Play()
    {

        if (musicManager != null)
        {

            musicManager.StopMainMenuMusic();
            SceneManager.LoadScene(gameScene);

        }

    }

    public void Quit()
    {

        Debug.Log("Player has quit the game");
        Application.Quit();

    }

}