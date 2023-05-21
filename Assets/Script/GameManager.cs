using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class GameData
    {
        public SOPlayer Player1;
        public SOPlayer Player2;
        public Winner winner;
        public string description;
    }
    [Serializable]
    public class tile
    {
        public GameObject tilePrefab;
        public int size;
    }
    public enum Winner
    {
        Tie,
        Player1,
        Player2,
    }

    public enum TurnState
    {
        Player1,
        Player2,
        GameOver
    }

    public static GameManager Instance;
    public float turnTimeLimit = 60f;
    public static int turnCounter;

    public static Winner winner;
    public static TurnState turnState;

    public SOPlayer Player1= null;
    public SOPlayer Player2= null;
    public tile[] tiles= null;

    public static bool isGameOver = false;
    public static bool isGamePause = false;

    public UnityEvent player1Turn;
    public UnityEvent player2Turn;

    GameData gameData = new GameData() //saved data
    {
        Player1 = null,
        Player2 = null,
        winner = Winner.Tie,
        description = ""
    };


    /* CloudSave cloudSave;*/ //id player = username1 + username2

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Lix")
        {
            NewGame();
        }

    }
    //NewGame
    public void NewGame()
    {
        Player1.resetPlayer(tiles, "player1");
        Player2.resetPlayer(tiles, "player2");
        isGameOver = false;
        winner = Winner.Tie;
        turnCounter = 1;
        turnState = TurnState.Player1;
        Player1.turnStart(turnTimeLimit);
        player1Turn?.Invoke();
        gameData.Player1 = Player1;
        gameData.Player2 = Player2;
        SoundManager.Instance.PlaySFX("NextLevel");
        //SceneManager.LoadScene("Arena");
    }

    // Update is called once per frame
    void Update()
    {
        switch (turnState)
        {
            case TurnState.Player1:
                Player1.update();
                break;
            case TurnState.Player2:
                Player2.update();
                break;
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCustomScene()
    {
        SceneManager.LoadScene("Custom");
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOver()
    {
        Debug.Log("gameover");
        switch (winner)
        {
            case Winner.Tie:
                SoundManager.Instance.PlaySFX("Tie");
                break;
            default:
                SoundManager.Instance.PlaySFX("Win");
                break;
        }
        gameData.winner = winner;
        Debug.Log(winner + " is the winner");
    }

    public void OutOfTime()
    {
        SoundManager.Instance.PlaySFX("OutOfTime");
        nextTurn();
    }

    public void nextTurn()
    {
        if (Player1.tiles.Length > 0 || Player2.tiles.Length > 0)
        {
            turnCounter++;
            switch (turnState)
            {
                case TurnState.Player1:
                    if (Player2.tiles.Length > 0)
                    {
                        player2Turn?.Invoke();
                        Player2.turnStart(turnTimeLimit);
                        turnState = TurnState.Player2;
                    }
                    else
                    {
                        player1Turn?.Invoke();
                        Player1.turnStart(turnTimeLimit);
                        turnState = TurnState.Player1;
                    }
                    break;
                case TurnState.Player2:
                    if (Player1.tiles.Length > 0)
                    {
                        player1Turn?.Invoke();
                        Player1.turnStart(turnTimeLimit);
                        turnState = TurnState.Player1;
                    }
                    else
                    {
                        player2Turn?.Invoke();
                        Player2.turnStart(turnTimeLimit);
                        turnState = TurnState.Player1;
                    }
                    break;
            }
        }
        else
        {
            turnState = TurnState.GameOver;
            if (Player1.score > Player2.score)
            {
                winner = Winner.Player1;
            }
            else if (Player1.score < Player2.score)
            {
                winner = Winner.Player2;
            }
            else
            {
                if (Player1.totalTimeNeeded > Player2.totalTimeNeeded)
                {
                    winner = Winner.Player2;
                }
                else if (Player2.totalTimeNeeded > Player1.totalTimeNeeded)
                {
                    winner = Winner.Player1;
                }
                else
                {
                    winner = Winner.Tie;
                }
            }
            GameOver();
        }

    }

    #region pause
    public void updateSettingWindow()
    {
        SettingsWindow.Instance.setActive(!SettingsWindow.Instance.getActiveSelf());
        if (!SettingsWindow.Instance.getActiveSelf())
        {
            resumeGame();
        }
        else
        {
            pauseGame();
        }
    }
    public void pauseGame()
    {
        Time.timeScale = 0f;
        isGamePause = true;
        Debug.Log("pause");

    }
    public void resumeGame()
    {
        Time.timeScale = 1.0f;
        isGamePause = false;
        Debug.Log("Resume");
    }
    #endregion


    public void QuitGame()
    {
        //cloudSave.OnClickSignOut();
        Application.Quit();
    }
    /*public void updateName(TMP_InputField text)
    {
        if (cloudSave == null)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();
        }
        username = text.text;
        cloudSave.OnClickSwitchProfile();
    }

    public void signIn()
    {
        if (cloudSave == null)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();
        }
        cloudSave.OnClickSignIn();
    }
    public async void saveData()
    {
        playerData.timeLeft = timeLeft;
        playerData.username = username;
        await cloudSave.ForceSaveObjectData($"Save_{saveCount}", playerData);
    }*/

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
