using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSceneManager : MonoBehaviour
{
    
    public Sprite[] tutorialImage;
    public SpriteRenderer BG;
    int curPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        BG.sprite = tutorialImage[curPage];
    }

    public void Next() {
        int curIndex = Array.IndexOf(tutorialImage, BG.sprite);
        if (curIndex >= tutorialImage.Length)
        {
            Debug.Log("Tutorial - IndexNotFound");
        }
        else
        {
            curPage += 1;
            if(curPage >= tutorialImage.Length)
            {
                curPage-=tutorialImage.Length;
            }
            BG.sprite = tutorialImage[curPage];
        }

    } 

    public void Prev()
    {
        int curIndex = Array.IndexOf(tutorialImage, BG.sprite);
        if (curIndex <= 0)
        {
            Debug.Log("Tutorial - IndexNotFound");
        }
        else
        {
            curPage -= 1;
            if(curPage< 0)
            {
                curPage+=tutorialImage.Length;
            }
            BG.sprite = tutorialImage[curPage];
        }
    }
    public void Done()
    {
        GameManager.Instance.NewGame();
    }

    public int getCurIndex()
    {
        return curPage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
