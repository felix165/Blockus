using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextRenderScript : MonoBehaviour
{
    public enum Opsi
    {
        curTimeLimit,
        turnCounter,
        username1,
        username2,
        turnState,
        winner,
        scorePlayer1,
        scorePlayer2,

    }

    public Opsi option;
    private TMP_Text textObj;


    // Start is called before the first frame update
    void Start()
    {
        textObj= GetComponent<TMP_Text>();  
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (option)
        {
            case Opsi.curTimeLimit:
                if(GameManager.turnState == GameManager.TurnState.Player1)
                {
                    textObj.text = formatTime((int)GameManager.Instance.blokusManager.playerList[0].TotalTimeLeft);
                        //Player1.turnTimeLeft).ToString("00");
                }else if (GameManager.turnState == GameManager.TurnState.Player2)
                {
                    textObj.text = formatTime((int)GameManager.Instance.blokusManager.playerList[1].TotalTimeLeft);
                }
                else
                {
                    textObj.text = "";
                }

                break;
            case Opsi.turnCounter:
                textObj.text = GameManager.turnCounter.ToString("0");
                break;
            case Opsi.username1:
                textObj.text = GameManager.Instance.blokusManager.playerList[0].Name;
                break;
            case Opsi.username2:
                textObj.text = GameManager.Instance.blokusManager.playerList[1].Name;
                break;
            case Opsi.turnState:
                textObj.text = GameManager.turnState.ToString();
                break;
            case Opsi.winner:
                textObj.text = GameManager.winner.ToString();
                break;
            case Opsi.scorePlayer1:
                textObj.text = ((int)GameManager.Instance.blokusManager.playerList[0].Score).ToString("00");
                break;
            case Opsi.scorePlayer2:
                textObj.text = ((int)GameManager.Instance.blokusManager.playerList[1].Score).ToString("00");
                break;
            default:
                Debug.Log("Option hasn't defined yet");
                return;

        }

    }
    string formatTime(float time)
    {
        int minute = (int) time / 60;
        int second = (int) time % 60;
        if (time <= 0)
        {
            return "00:00";
        }
        if (minute >= 10)
        {
            if (second >= 10)
            {
                return minute + ":" + second;
            }
            else
            {
                return minute + ":0" + second;
            }
        }
        else
        {
            if (second >= 10)
            {
                return "0" + minute + ":" + second;
            }
            else
            {
                return "0" + minute + ":0" + second;

            }

        }
    }

}
