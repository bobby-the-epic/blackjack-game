using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    const int deckSize = 52;
    int playerCardNum, dealerCardNum;
    bool playerBust, dealerBust;
    float xStep = 0.25f;
    float zStep = 0.5f;

    [SerializeField]
    int playerPoints, dealerPoints;
    [SerializeField]
    GameObject cardPrefab, deckObject, dealerCardsPos, playerCardsPos,
    mainMenu, gameOverMenu, playModeButtons;
    [SerializeField]
    TextMeshProUGUI playerScore, dealerScore;
    [SerializeField]
    Sprite[] cardTextures;
    [SerializeField]
    List<GameObject> playerCards, dealerCards, deck;

    void Start()
    {
        deck = new List<GameObject>(deckSize);
        DeckInit();
        ShuffleDeck();
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
        playerBust = false;
        dealerBust = false;
        ShuffleDeck();
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
    void AddPoints(bool isPlayer)
    {
        int points = 0;
        int aceCount = 0;
        List<GameObject> hand = isPlayer ? playerCards : dealerCards;
        foreach (GameObject card in hand)
        {
            /* 
                Iterate through the player's/dealer's cards and calculate the points.
                Aces are worth 0 at first, then considered after all of the other cards 
                in the hand are added to the point total.
            */
            Card cardScript = card.GetComponent<Card>();
            points += cardScript.pointValue;
            if (cardScript.isAce)
                aceCount++;
        }
        for (int counter = 0; counter < aceCount; counter++)
        {
            // Calculates the value of the aces.
            if (points > 10)
                points += 1;
            else
                points += 11;
        }
        if (isPlayer)
        {
            playerPoints = points;
            if (playerPoints > 21)
                playerBust = true;
            playerScore.text = string.Format("You:\n{0}", playerPoints);
        }
        else
        {
            dealerPoints = points;
            if (dealerPoints > 21)
                dealerBust = true;
            dealerScore.text = string.Format("Dealer:\n{0}", dealerPoints);
        }
    }
    public void DrawCard(bool isPlayer)
    {
        Vector3 cardPos;
        Quaternion cardRot;
        int cardNum;
        GameObject newCard = deck[0];
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
        AddPoints(isPlayer);
    }
    public void PlayGame()
    {
        mainMenu.SetActive(false);
        deckObject.SetActive(true);
        playerScore.gameObject.SetActive(true);
        dealerScore.gameObject.SetActive(true);
        DrawCard(false);
        DrawCard(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
