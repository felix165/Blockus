using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    public TMP_Text playerName1;
    public TMP_Text playerName2;
    public TMP_Text scoreP1;
    public TMP_Text scoreP2;
    public GameObject winObjP1;
    public GameObject winObjP2;
    // Start is called before the first frame update
    void Start()
    {
        playerName1.text = Blokus.playerList[0].Name;
        playerName2.text= Blokus.playerList[1].Name;
        int scorep1 = Blokus.playerList[0].Score;
        int scorep2 = Blokus.playerList[1].Score;
        scoreP1.text = scorep1.ToString();
        scoreP2.text = scorep2.ToString();
        if(scorep1 > scorep2)
        {
            winObjP1.SetActive(true);
            winObjP2.SetActive(false);
        }else if( scorep1 == scorep2) 
        {
            winObjP1.SetActive(true);
            winObjP2.SetActive(true);
        }
        else
        {
            winObjP1.SetActive(false);
            winObjP2.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
