using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    const int deckSize = 52;
    int playerPoints, playerCardNum, dealerPoints, dealerCardNum;
    float xStep = 0.25f;
    float zStep = 0.5f;
    GameObject newCard;

    [SerializeField]
    GameObject cardPrefab, deckObject, dealerCardsPos, playerCardsPos;
    [SerializeField]
    Sprite[] cardTextures;
    [SerializeField]
    List<GameObject> playerCards, dealerCards, deck;

    void Start()
    {
        deck = new List<GameObject>(deckSize);
        DeckInit();
        ShuffleDeck();
        DrawCard(false);
        DrawCard(true);
        DrawCard(false);
        DrawCard(true);
        DrawCard(false);
        DrawCard(false);
        DrawCard(true);
    }
    void Update()
    {
    }
    void Restart()
    {
        // Sends the player's and dealer's cards back to the deck,
        // clears the lists, and sets all relevant variables to 0.

        foreach (GameObject card in playerCards)
        {
            deck.Add(card);
            card.transform.position = deckObject.transform.position;
            card.transform.SetParent(gameObject.transform);
            card.SetActive(false);
        }
        foreach (GameObject card in dealerCards)
        {
            deck.Add(card);
            card.transform.position = deckObject.transform.position;
            card.transform.SetParent(gameObject.transform);
            card.SetActive(false);
        }

        playerCards.Clear();
        dealerCards.Clear();
        dealerCardNum = 0;
        playerCardNum = 0;
        dealerPoints = 0;
        playerPoints = 0;
    }
    void DeckInit()
    {
        // Spawns the cards inside the deck object and deactivates them until they are needed.
        GameObject card;
        Card cardScript;
        for (int counter = 0; counter < deckSize; counter++)
        {
            card = Instantiate(cardPrefab, deckObject.transform.position, deckObject.transform.rotation);
            cardScript = card.GetComponent<Card>();
            cardScript.SetCard(cardTextures[counter]);
            card.SetActive(false);
            card.transform.SetParent(gameObject.transform);
            deck.Add(card);
        }
    }
    void ShuffleDeck()
    {
        /*
            Creates a temporary list of the card textures and assigns a random index of that list
            to each of the cards in the deck. After a texture has been assigned, the element at that
            index is removed from the list. This is so that each card in the deck is assigned a unique texture.
        */
        int index;
        List<Sprite> temp = new List<Sprite>(cardTextures);
        Card cardScript;
        foreach (GameObject card in deck)
        {
            index = Random.Range(0, temp.Count);
            cardScript = card.GetComponent<Card>();
            cardScript.SetCard(temp[index]);
            temp.RemoveAt(index);
        }
    }
    void DrawCard(bool isPlayer)
    {
        Vector3 cardPos;
        Quaternion cardRot;
        int cardNum;
        newCard = deck[0];
        deck.RemoveAt(0);

        if (isPlayer)
        {
            playerCardNum++;
            cardNum = playerCardNum;
            cardPos = playerCardsPos.transform.position;
            cardRot = playerCardsPos.transform.rotation;
            playerCards.Add(newCard);
        }
        else
        {
            dealerCardNum++;
            cardNum = dealerCardNum;
            cardPos = dealerCardsPos.transform.position;
            cardRot = dealerCardsPos.transform.rotation;
            dealerCards.Add(newCard);
        }
        if (cardNum > 1)
        {
            // Moves the card's position diagonally if there is more than one
            // so that they don't stack on top of each other.
            cardPos.x += (isPlayer ? xStep : -xStep) * (cardNum - 1);
            cardPos.z += (isPlayer ? zStep : -zStep) * (cardNum - 1);
        }
        newCard.SetActive(true);
        newCard.transform.position = cardPos;
        newCard.transform.rotation = cardRot;
        // Sets the dealer's hidden card upside down.
        if (!isPlayer && dealerCardNum == 1)
            newCard.transform.Rotate(Vector3.forward * 180);
        newCard.transform.SetParent(isPlayer ? playerCardsPos.transform : dealerCardsPos.transform);
    }
}
