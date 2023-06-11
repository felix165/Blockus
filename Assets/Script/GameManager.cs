using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
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
        public int player1Score;
        public int player2Score;
        public string winner;
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
    public static bool isInputEnabled = true;

    public Blokus blokusManager;
    public float playerTimeTotal = 300f;

    [HideInInspector]
    public static string usernamePlayer1 = "Player1";
    [HideInInspector]
    public static string usernamePlayer2 = "Player2";

    public static string profileName = "";

    CloudSave cloudSave;
    private int saveCount = 0;
    private string saveID = "";

    


    GameData gameData = new GameData() //saved data
    {
        player1Score= 0,
        player2Score= 0,
        winner = Winner.Tie.ToString(),
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
        cloudSave = this.gameObject.GetComponent<CloudSave>();
    }
    //NewGame
    public void NewGame()
    {
        if (cloudSave != true)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();
        }
        print(usernamePlayer1+ " " + usernamePlayer2);
        setPlayerName(usernamePlayer1, usernamePlayer2);
        winner = Winner.Tie;
        turnCounter = 1;
        turnState = TurnState.Player1;

        SceneManager.LoadScene(2);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            updateSettingWindow();
        }
    }

    public void LoadMainMenuScene()
    {
        if (cloudSave != true)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();
        }
        if (cloudSave == true)
        {
            cloudSave.OnClickSignOut();
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCustomScene()
    {
        SceneManager.LoadScene("Custom");
    }

    public void LoadTutorialScene()
    {
        signIn();
        
        if (cloudSave == true)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            Debug.Log("you haven't sign in yet");
        }
        print(usernamePlayer1+ " " + usernamePlayer2);

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
        saveData();
        Debug.Log(winner + " is the winner");
        SceneManager.LoadScene("GameOver");
    }

    public void nextTurn(int currentIndex)
    {
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
        isInputEnabled = false;
        Debug.Log("pause");

    }
    public void resumeGame()
    {
        Time.timeScale = 1.0f;
        isGamePause = false;
        isInputEnabled = true;
        Debug.Log("Resume");
    }
    #endregion


    public void QuitGame()
    {
        if(cloudSave != true)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();  
        }
        if (cloudSave == true)
        {
            cloudSave.OnClickSignOut();
            Application.Quit();
        }
    }

    public void onUsernamePlayer1Change(TMP_InputField text)
    {
        usernamePlayer1 = text.text;
        setProfileName();
    }
    public void onUsernamePlayer2Change(TMP_InputField text)
    {
        usernamePlayer2 = text.text;
        setProfileName();
    }
    public void setProfileName()
    {
        if (cloudSave == null)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();
        }
        profileName = usernamePlayer1 + "-VS-" + usernamePlayer2;
        cloudSave.OnClickSwitchProfile();
        print(usernamePlayer1+ " " + usernamePlayer2);

    }

    public void signIn()
    {
        if (cloudSave == null)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();
        }
        cloudSave.OnClickSignIn();
        
    }
/*    public async void saveData()
    {
        gameData.player1Score = Blokus.playerList[0].Score;
        gameData.player2Score = Blokus.playerList[1].Score;
        gameData.winner = winner.ToString();
        gameData.description = profileName;
        await cloudSave.ForceSaveObjectData($"Save_{saveCount}_", gameData);
    }*/
    public async void saveData()
    {
        gameData.player1Score = Blokus.playerList[0].Score;
        gameData.player2Score = Blokus.playerList[1].Score;
        gameData.winner = winner.ToString();
        gameData.description = profileName;
        await cloudSave.ForceSaveObjectData($"Save_{saveID}_", gameData);
    }
    public void saveCountCheck()
    {
        if (cloudSave == null)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();
        }
        saveCount = cloudSave.KeysCount().Result;
        print(saveCount);
    }
    public void increaseSaveCount()
    {
        saveCount++;
    }
    public void generateUniqueID()
    {
        string newBackstageItemID = System.Guid.NewGuid().ToString();
        saveID = newBackstageItemID;
    }

    void Awake()
    {

        if (cloudSave == null)
        {
            cloudSave = this.gameObject.GetComponent<CloudSave>();
        }

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
