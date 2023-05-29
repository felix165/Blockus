using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class GameData
    {
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
    public static int turnCounter;
    public static Winner winner;
    public static TurnState turnState;
    public static bool isGamePause = false;

    public Blokus blokusManager;
    public float playerTimeTotal = 300f;

    [HideInInspector]
    public string usernamePlayer1 = "Player1";
    [HideInInspector]
    public string usernamePlayer2 = "Player2";


    GameData gameData = new GameData() //saved data
    {
        winner = Winner.Tie,
        description = ""
    };


    /* CloudSave cloudSave;*/ //id player = username1 + username2

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Arena")
        {
            /**/
            Debug.Log("Arena");
            NewGame();
        }
    }
    //NewGame
    public void NewGame()
    {
        setPlayerName(usernamePlayer1, usernamePlayer2);
        blokusManager.newGame();
        winner = Winner.Tie;
        turnCounter = 1;
        turnState = TurnState.Player1;

        foreach (Player p in blokusManager.playerList)
        {
            p.TotalTimeLeft = playerTimeTotal;
        }
        //SceneManager.LoadScene("Arena");
    }

    public void setPlayerName(string player1 = "Player1", string player2 = "Player2")
    {
        List<Player> playersList = new List<Player>();
        BlokusColor color = (BlokusColor)(0 - DropdownColor.VALUE_CORRECTION);
        Player newPlayer = new Player(color, player1);
        newPlayer.TotalTimeLeft = playerTimeTotal;
        playersList.Add(newPlayer);


        color = (BlokusColor)(1 - DropdownColor.VALUE_CORRECTION);
        newPlayer = new Player(color, player2);
        newPlayer.TotalTimeLeft = playerTimeTotal;
        playersList.Add(newPlayer);


        PlayerList.Players = playersList;
    }

    // Update is called once per frame
    void Update()
    {

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
        turnState = TurnState.GameOver;
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

    public void nextTurn()
    {
        int currentIndex = blokusManager.playerList.IndexOf(blokusManager.currentPlayer);
        if(currentIndex == 0)
        {
            Debug.Log("Player1");
            turnState = TurnState.Player1;
        }else if (currentIndex == 1)
        {
            Debug.Log("Player2");
            turnState = TurnState.Player2;
        }
    }
    public void setWinnerState(int state)
    {
        if(state == 0)
        {
            winner = Winner.Tie;
        }else if (state == 1)
        {
            winner = Winner.Player1;
        }else if (state == 2)
        {
            winner = Winner.Player2;
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
