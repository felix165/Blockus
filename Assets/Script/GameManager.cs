using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    enum Winner
    {
        Tie,
        Player1,
        Player2,
    }

    enum TurnState
    {
        Player1,
        Player2,
    }

    public static GameManager Instance;
    public float turnTimeLimit = 60f;
    public static float timeLeft;
    public static int turnCounter;
    static Winner winner;
    static TurnState turnState;

    [SerializeField]
    private int tutorialScene = 1;
    [SerializeField]
    private int mainMenuScene = 0;
    [SerializeField]
    private int customScene = 2;
    [SerializeField]
    private int playScene = 3;

    public static bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            //NextTurn
            nextTurn();
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }
    //NewGame
    public void NewGame()
    {
        timeLeft = turnTimeLimit;
        isGameOver = false;
        winner= Winner.Tie;
        turnCounter= 0;
        turnState = TurnState.Player1;

        SoundManager.Instance.PlaySFX("NextLevel");
        SceneManager.LoadScene(playScene);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadCustomScene()
    {
        SceneManager.LoadScene(customScene);
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(tutorialScene);
    }

    public void GameOver()
    {
        switch (winner)
        {
            case Winner.Tie:
                SoundManager.Instance.PlaySFX("Tie");
                break;
            default:
                SoundManager.Instance.PlaySFX("Win");
                break;
        }
        Debug.Log(winner);
    }

    public void OutOfTime()
    {
        SoundManager.Instance.PlaySFX("OutOfTime");
        nextTurn();
    }

    public void nextTurn()
    {
        turnCounter++;
        timeLeft = turnTimeLimit;
        switch (turnState)
        {
            case TurnState.Player1:
                turnState= TurnState.Player2;
                break;
            case TurnState.Player2:
                turnState = TurnState.Player1;
                break;
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
