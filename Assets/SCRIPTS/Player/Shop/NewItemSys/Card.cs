using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : ScriptableObject
{
    public Item item;

    public Card(Item item)
    {
        this.item = item;
      
    }

    public Card(Card card)
    {
        this.item = card.item;
    }

    public Card()
    {

    }
}
