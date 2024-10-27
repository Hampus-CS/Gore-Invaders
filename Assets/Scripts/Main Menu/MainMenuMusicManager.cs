using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{

    AudioSource mainMenuMusic;

    // Plays music when the player enters the game.
    void Start()
    {
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        if (mainMenuMusic != null && !mainMenuMusic.isPlaying)
        {
            mainMenuMusic.Play();
        }
    }

    public void StopMainMenuMusic()
    {
        if (mainMenuMusic != null && mainMenuMusic.isPlaying)
        {
            mainMenuMusic.Stop();
        }
    }

}