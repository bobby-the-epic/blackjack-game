using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject deck;
    public Sprite[] cardTextures;

    int playerPoints, dealerPoints;
    SpriteRenderer cardSprite;
    GameObject newCard;
    Vector3 spawnPos;
    GameObject[] dealerCards;
    List<GameObject> playerCards;

    void Start()
    {
        dealerCards = new GameObject[2];
        // The spawn position of the cards is under the deck so it looks like a card is physically drawn.
        spawnPos = deck.transform.position;
        spawnPos.y = 1.74f;
        DrawCard();
    }
    void Update()
    {
    }
    void DrawCard()
    {
        int index = Random.Range(0, cardTextures.Length);
        newCard = Instantiate(cardPrefab, spawnPos, deck.transform.rotation);
        Card cardScript = newCard.GetComponent<Card>();
        cardScript.SetCard(cardTextures[index]);
    }
}
