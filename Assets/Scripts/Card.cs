using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int pointValue { get; private set; } = 0;
    public bool isAce { get; private set; } = false;
    public GameObject cardFront;
    public SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = cardFront.GetComponent<SpriteRenderer>();
    }
    public void SetCard(Sprite sprite)
    {
        /*
            Renames the gameobject to the name of the sprite texture, removes the prefix
            of the name (the prefix is the suit), and sets the point value of the card.
        */

        spriteRenderer.sprite = sprite;
        name = sprite.name;
        string cardNum = name.Remove(0, 1);
        int num = int.Parse(cardNum);

        switch (num)
        {
            // Aces are worth 0 so they can be 1 or 11 based on the other cards in play.
            case 1:
                isAce = true;
                pointValue = 0;
                break;
            case > 10:
                isAce = false;
                pointValue = 10;
                break;
            default:
                isAce = false;
                pointValue = num;
                break;
        }
    }
}
