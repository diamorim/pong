using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("GameStates Properties")]
    [SerializeField] private GameObject startGameScreen;

    [Header("Pong Objects")]
    [SerializeField] Transform[] players;
    [SerializeField] Transform[] playersOriginalPosition;
    [SerializeField] GameObject ball;
    private float ballSpeed;

    [Header("Cooldowns")]
    [SerializeField] float cooldownAfterPoint = 3f;
    [SerializeField] float cooldownToStart = 1f;

    [Header("UI")]
    [SerializeField] private GameObject[] pointScreens; 
    [SerializeField] private GameObject startText; 

    [Header("GameOver Properties")]
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject YouWinText;
    [SerializeField] private GameObject YouLoseText;


    public static GameManager instance;
    public event Action<int> OnGameOver;
    public event Action<int> OnPointMade;

    #endregion

    private void Awake()
    {
        if (instance == null || instance != this)
            instance = this;
        ChangeGameState(GameStates.start);
        if(ball != null )
            ballSpeed = ball.GetComponent<Ball>().speed;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    #region TriggerEvents
    private void OnEnable()
    {
        OnPointMade += (ID)=> StartCoroutine(OnMaKePoint(ID));
    }
    private void OnDisable()
    {
        OnPointMade -= (ID)=> StartCoroutine(OnMaKePoint(ID));
    }
    public void TriggerGameOver(int ID)
    {
        OnGameOver.Invoke(ID);
    }
    public void TriggerOnPointMade(int ID)
    {
        OnPointMade.Invoke(ID);
    }
    #endregion

    #region Game States Management

    enum GameStates
    {
        start,
        inGame,
        madePoint,
        gameOver,

    }
    private GameStates currentGameState;

    private void ChangeGameState(GameStates newState)
    {
        currentGameState = newState;

        switch (currentGameState)
        {
            case GameStates.start:
                GameStart();

                break;

            case GameStates.inGame:
                InGame();

                break;

            case GameStates.gameOver:
                OnGameOver += GameOver;

                break;

            default:
                break;
        }
    }
    private void Update()
    {
        if (currentGameState == GameStates.start && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
            ChangeGameState(GameStates.inGame);
        }
    }
    #region Game States
    private void GameStart()
    {
        startText.SetActive(true);
        Time.timeScale = 0;
        startGameScreen.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeGameState(GameStates.inGame);
    }
    private void StartGame()
    {
        print("game started");
        startText.SetActive(false);
        Time.timeScale = 1;
        startGameScreen.SetActive(false);
        print("score board on");
    }
    private void InGame()
    {
        Time.timeScale = 1;
    }
    private IEnumerator OnMaKePoint(int ID)
    {
        print("made point");
        OnGameOver += GameOver;
        Time.timeScale = 0;
        print(currentGameState);
        pointScreens[ID].SetActive(true);
        yield return new WaitForSecondsRealtime(cooldownAfterPoint);
        if (currentGameState == GameStates.inGame)
        {
            print("if activated");
            ResetingGame();
            yield return new WaitForSecondsRealtime(cooldownToStart);
            ChangeGameState(GameStates.inGame);
        }
        print("totally made point");
    }
    public void GameOver(int ID)
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        GameOverScreen.SetActive(true);
        if (ID == 0)
            YouWinText.SetActive(true);
        else
            YouLoseText.SetActive(true);
    }
    #endregion
    #endregion

    void ResetingGame()
    {
        Array.ForEach(pointScreens, screens => screens.SetActive(false));
        Ball ballScript = ball.GetComponent<Ball>();
        for (int i = 0;i < 2; i++)
        {
            players[i].position = playersOriginalPosition[i].position;
            ball.transform.position = new Vector2(0, 0);
            ballScript.speed = ballSpeed;
        }

    }

    #region Score Management

    #endregion
    #region Scene Management
    public void ReloadScene()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int ID)
    {
        SceneManager.LoadScene(ID);
    }
    #endregion
}
