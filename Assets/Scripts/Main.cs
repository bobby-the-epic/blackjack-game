using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject card;
    public GameObject spawnPosition;
    public Sprite[] cardTextures;

    SpriteRenderer cardSprite;

    void Start()
    {
        //Instantiate card
        Instantiate(card, spawnPosition.transform.position, spawnPosition.transform.rotation);
    }
    void Update()
    {

    }
}
