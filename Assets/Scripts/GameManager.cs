using UnityEngine;
using System.Collections.Generic;
using TMPro;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;
    public GameObject victoryPrefab;
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    // To make sure bunkers are located on these positions and it can't be changed.
    private readonly Vector3[] bunkerPositions = { new Vector3(-10, -9, 0), new Vector3(-3.5f, -9, 0), new Vector3(3.5f, -9, 0), new Vector3(10, -9, 0) };

    private int wavesCleared = 0;

    public GameObject deathScreen;
    public TextMeshProUGUI[] scoreText;
    public List<GameObject> hearts;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        deathScreen.gameObject.SetActive(false);
        SetScore(0);
        SetLives(3);
        mysteryShip.msLives = 3;

        NewRound();
    }

    private void NewRound()
    {
        wavesCleared += 1;
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);
        ResetBunkers();
        invaders.speed = 1 + wavesCleared * 0.75f;
        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        invaders.gameObject.SetActive(false);
    }

    private void SetScore(int playerScore)
    {
        score = playerScore;
        scoreText[0].text = $"Score: {score}"; // score during playing the game.
        scoreText[1].text = $"Score: {score}"; // score in death screen.
        Debug.Log($"Score: {score}");

        if (score > 0 && score % 100 == 0)
        {
            invaders.IncreaseSpeed();
        }
    }

    private void SetLives(int lives)
    {
        // Activate lives.
        hearts[0].SetActive(true);
        hearts[1].SetActive(true);
        hearts[2].SetActive(true);
        Debug.Log($"Lives: {lives}");
    }

    public void Health()
    {
        // Life funktion for player.
        {
            lives -= 1;

            if (lives == 2)
            {
                hearts[0].SetActive(false);
                player.spRend.sprite = player.playerLifeSprites[1];
                Instantiate(player.PlayerHitSound, transform.position, Quaternion.identity);
            }
            else if (lives == 1)
            {
                hearts[1].SetActive(false);
                player.spRend.sprite = player.playerLifeSprites[2];
                Instantiate(player.PlayerHitSound, transform.position, Quaternion.identity);
            }
            else if (lives == 0)
            {
                hearts[2].SetActive(false);
                Instantiate(player.PlayerHitSound, transform.position, Quaternion.identity);
                OnPlayerKilled(player);
            }

            Debug.Log($"Player lives remaining: {lives}");
        }

    }

    public void OnPlayerKilled(Player player)
    {
        
        player.gameObject.SetActive(false);
        invaders.gameObject.SetActive(false);
        mysteryShip.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;

    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);
        
        if (invader.invaderType == 1)
        {
            SetScore(score + 10);
        }
        else if (invader.invaderType == 2)
        {
            SetScore(score + 20);
        }
        else if (invader.invaderType == 3)
        {
            SetScore(score + 30);
        }
        
        if (invaders.GetInvaderCount() == 0)
        {
            NewRound();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        mysteryShip.gameObject.SetActive(false);
        SetScore(score + 250);
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);
            OnPlayerKilled(player);
            Time.timeScale = 0f;
        }
    }

    private void ResetBunkers()
    {
        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].transform.position = bunkerPositions[i];
            bunkers[i].ResetBunker();
        }
    }

}
