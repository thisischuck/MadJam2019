using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController playerPrefab;
    public Camera PlayerCamera;
    private PlayerController currentPlayer;
    public Vector3 SpawnPosition;

    public float highscore = 0;
    public Canvas UI;
    public GameObject deathScreen;
    public GameObject scoreUI;
    public TMP_Text txtScore;
    public TMP_Text txtFinalScore;
    public AudioManager aS;

    public GameObject RNG;
    public GameObject Bus;
    private bool gameRunning;
    public bool isAlive = true;

    private void Awake()
    {
        if (Instance == null)
        {
            GameManager.Instance = this;
            gameRunning = false;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void Start()
    {
        aS = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (gameRunning)
        {
            RNG.SetActive(true);
            if (Bus)
                Bus.SetActive(true);
            UI.gameObject.SetActive(true);
            txtScore.text = "Score: " + (int)highscore;
            if (!isAlive)
            {
                Time.timeScale = 0f;
                UI.gameObject.SetActive(true);
                deathScreen.gameObject.SetActive(true);
                txtFinalScore.text = "Final Score: " + (int)highscore;
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameRunning)
        {
            highscore += 10 * Time.deltaTime;
        }
    }

    public void StartGame()
    {
        gameRunning = true;
        aS.Play("rain");
        SetUpPlayer();
    }

    public void SetUpPlayer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
            currentPlayer = Instantiate(playerPrefab);
        else currentPlayer = player;

        currentPlayer.transform.position = SpawnPosition;
        currentPlayer.isMovable = true;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndGame(bool won)
    {
        //UIManager.Instance.SetEnd(true, pickedUpItems);
        //FindObjectOfType<AudioManager>().Play("Moan");
        //currentPoop.transform.position = currentPlayer.transform.position - (Vector3.down * 0.5f);
        gameRunning = false;
        currentPlayer.isMovable = false;
    }
}
