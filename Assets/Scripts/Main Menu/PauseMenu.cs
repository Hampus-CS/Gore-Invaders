using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public int mainMenu;
    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }

    }
    
    void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        player.gameObject.SetActive(false);
        invaders.gameObject.SetActive(false);
        mysteryShip.gameObject.SetActive(false);

    }

    public void Resume ()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        player.gameObject.SetActive(true);
        invaders.gameObject.SetActive(true);
        mysteryShip.gameObject.SetActive(true);

    }
    
    public void QuitToTitle()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);

    }

}
