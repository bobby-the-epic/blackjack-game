using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject spawnPosition;
    public Sprite[] cardTextures;

    SpriteRenderer cardSprite;

    void Start()
    {
        //Instantiate card
        Vector3 spawnPos = spawnPosition.transform.position;
        spawnPos.y = 1.75f;
        Instantiate(cardPrefab, spawnPos, spawnPosition.transform.rotation);
    }
    void Update()
    {

    }
}
