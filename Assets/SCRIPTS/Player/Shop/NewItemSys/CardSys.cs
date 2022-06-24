using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSys : MonoBehaviour
{
    public Card Card;

    public Image CardItemIcon;
    public Image CardQualityItemIcon;
    public Text ItemName_Text;
    public Text ItemDescription_Text;
    public Button SwapButton;
    public void CardUI()
    {
        CardItemIcon.sprite = Card.item.ItemIcon;
        CardQualityItemIcon.sprite = Card.item.ItemQuality;
        ItemName_Text.text = Card.item.ItemName;
        ItemDescription_Text.text = Card.item.ItemDescription;
    }
}
