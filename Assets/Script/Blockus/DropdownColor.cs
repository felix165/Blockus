using UnityEngine;
using TMPro;
using Assets;

public class DropdownColor : MonoBehaviour
{
    public TMP_Dropdown DropdownColors;
    public int LastSelectedValue = 0;
    public Sprite[] color_;

    // The value of the color selected in the dropdown is not the same of the BlokusColor, so we have to correct it
    public const int VALUE_CORRECTION = -2;

    void Start()
    {
        DropdownColors.onValueChanged.AddListener(delegate {
            DropdownValueChanged(DropdownColors);
        }); 
    }

    /// <summary>
    /// Change the background color of the dropdown according to its new value selected
    /// </summary>
    public void DropdownValueChanged(TMP_Dropdown dropdown) {
        switch (dropdown.value) {
            case (int)BlokusColor.Player1 + VALUE_CORRECTION:
                //dropdown.image.color = Color.green;
                dropdown.image.sprite = color_[0];
                break;
            case (int)BlokusColor.Player2 + VALUE_CORRECTION:
                //dropdown.image.color = Color.red;
                dropdown.image.sprite = color_[1];
                break;
            default:
                dropdown.image.sprite = color_[1];
                break;
        }
        LastSelectedValue = dropdown.value;
    }
}
