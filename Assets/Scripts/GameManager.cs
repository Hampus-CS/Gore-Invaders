using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using TMPro;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;
    
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    public TextMeshProUGUI scoreText;
    public GameObject heartPrefab;
    public Transform livesContainer;
    private List<GameObject> hearts = new List<GameObject>();

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

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }

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
        scoreText.text = $"Score: {score}";
        Debug.Log($"Score: {score}");

        if (score > 0 && score % 100 == 0)
        {
            invaders.IncreaseSpeed();
        }
    }

    private void SetLives(int playerlives)
    {
        lives = playerlives;
        Debug.Log($"Lives: {lives}");
    }

    public void OnPlayerKilled(Player player)
    {

        player.gameObject.SetActive(false);

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
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);
            OnPlayerKilled(player);
        }
    }

}
