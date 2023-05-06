using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    Graphic[] graphic;
    // Start is called before the first frame update
    void Start()
    {
        graphic = this.gameObject.GetComponentsInChildren<Graphic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fade(float alpha=0)
    {
        graphic = this.gameObject.GetComponentsInChildren<Graphic>();
        foreach (var g in graphic)
        {
            Button btn = g.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = false;
            }
            var newColor = g.color;
            newColor.a = alpha;
            g.color = newColor;

        }
    }
    public void unFade()
    {
        graphic = this.gameObject.GetComponentsInChildren<Graphic>();
        foreach (var g in graphic)
        {
            Button btn = g.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = true;
            }
            var newColor = g.color;
            newColor.a = 1;
            g.color = newColor;

        }
    }
}
