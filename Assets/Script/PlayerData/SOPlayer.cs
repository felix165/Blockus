using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName= "NewPlayerData", menuName= "DataSystem/PlayerData")]
public class SOPlayer : ScriptableObject
{
    public string username;
    public int score=0;
    public GameManager.tile[] tiles;
    public float turnTimeLeft;
    public float totalTimeNeeded;


    public void resetPlayer(GameManager.tile[] tiles = null, string username="anonymous")
    {
        this.username = username;
        score = 0;
        totalTimeNeeded= 0;
        if(tiles != null)
        {
            tiles = null;
        }
        else
        {
            Array.Copy(tiles, this.tiles, tiles.Length);
        }
    }
    public void turnStart(float turnTimeLimit)
    {
        /*if(tiles.Length<=0)
        {
            GameManager.Instance.nextTurn();
            return;
        }*/
        turnTimeLeft = turnTimeLimit;
        Debug.Log(this.username + " turn");
    }
    public void update()
    {
        turnTimeLeft -= Time.deltaTime;
        totalTimeNeeded += Time.deltaTime;
        if (turnTimeLeft <= 0)
        {
            Debug.Log("time out" + this.username);
            GameManager.Instance.OutOfTime();
        }
    }
    public void tileUsed(GameManager.tile tile)
    {
        turnTimeLeft = 0;
        score += tile.size;
        var tempTiles = tiles.ToList();
        tempTiles.Remove(tile);
        tiles = tempTiles.ToArray();
    }

}
