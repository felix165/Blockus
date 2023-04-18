using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextRenderScript : MonoBehaviour
{
    public enum Opsi
    {
        curTimeLimit,
        turnCounter,

    }

    public Opsi option;
    public Text textObj;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        switch (option)
        {
            case Opsi.curTimeLimit:
                textObj.text = formatTime(GameManager.turnCounter);
                break;
            case Opsi.turnCounter:
                textObj.text = GameManager.turnCounter.ToString("0");
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
