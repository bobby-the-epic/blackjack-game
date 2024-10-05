using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject cardFront;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = cardFront.GetComponent<SpriteRenderer>();
    }
    public void SetCard(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        name = sprite.name;
    }
}
