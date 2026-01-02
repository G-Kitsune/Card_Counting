using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardManager : MonoBehaviour
{
    public List<int> cardDeck = new List<int>();
    public List<int> cardData = new List<int>();
    public List<int> cardDataWithoutHand = new List<int>();

    public List<int> playerHandCard = new List<int>();
    public List<int> aiHandCard = new List<int>();

    public int revealedCard = (int)EnumCard.NormalDog;

    public List<GameObject> objectCards = new List<GameObject>();

    public GameObject deckCard;
    public GameObject gameCard;
    public GameObject graveCard;

    

    public void FillOnNullData()
    {
        if (PlayerPrefs.GetInt("NormalCount") == null)
        {
            PlayerPrefs.SetInt("NormalCount", 10);
        }
        if (PlayerPrefs.GetInt("GroupCount") == null)
        {
            PlayerPrefs.SetInt("GroupCount", 2);
        }
        if (PlayerPrefs.GetInt("ShadowCount") == null)
        {
            PlayerPrefs.SetInt("ShadowCount", 2);
        }
        if (PlayerPrefs.GetInt("DogCount") == null)
        {
            PlayerPrefs.SetInt("DogCount", 2);
        }
    }

    public void LoadData()
    {
        
        for (int i = 0; i < 7; i++)
        {
            cardData.Add(0);
        }
        
        
        cardData[(int)EnumCard.NormalSheep] = PlayerPrefs.GetInt("NormalCount");
        cardData[(int)EnumCard.NormalWolf] = PlayerPrefs.GetInt("NormalCount");

        cardData[(int)EnumCard.GroupSheep] = PlayerPrefs.GetInt("GroupCount");
        cardData[(int)EnumCard.GroupWolf] = PlayerPrefs.GetInt("GroupCount");

        cardData[(int)EnumCard.ShadowSheep] = PlayerPrefs.GetInt("ShadowCount");
        cardData[(int)EnumCard.ShadowWolf] = PlayerPrefs.GetInt("ShadowCount");

        cardData[(int)EnumCard.NormalDog] = PlayerPrefs.GetInt("DogCount");
        
    }

    public void ShuffleDeck()
    {
        //플레이어 핸드에 있는 카드 제외

        for (int i = 0; i < 7; i++)
        {
            cardDataWithoutHand.Add(0);
        }

        for (int i = 0; i < cardData.Count; i++)
        {
            cardDataWithoutHand[i] = cardData[i] - playerHandCard.FindAll(cardType => cardType == i).Count - aiHandCard.FindAll(cardType => cardType == i).Count;
        }
        
        //카드 채우기
        for (int i = 0; i < cardDataWithoutHand.Count; i++)
        {
            for (int j = 0; j < cardDataWithoutHand[i]; j++)
            {
                cardDeck.Add(i);
            }
        }
        
        //셔플 알고리즘
        for (int i = 0; i < cardDeck.Count - 1; i++)
        {
            int indexToExchange;
            indexToExchange = Random.Range(i, cardDeck.Count);

            int tempExchange;
            tempExchange = cardDeck[i];
            cardDeck[i] = cardDeck[indexToExchange];
            cardDeck[indexToExchange] = tempExchange;
        }
        
    }

    public void FirstDrawCard(int player)
    {
        int deckTopCard = cardDeck[0];

        if (player == (int)EnumPlayer.Player)
        {
            playerHandCard.Add(deckTopCard);
            cardDeck.RemoveAt(0);
        }

        else if (player == (int)EnumPlayer.AI)
        {
            aiHandCard.Add(deckTopCard);
            cardDeck.RemoveAt(0);
        }
    }

    public void DrawCard(int player)
    {
        revealedCard = cardDeck[0];
        cardDeck.RemoveAt(0);
    }
    
    public void ExchangeCard(int player, int cardIndex)
    {
        

        int cardToWaste = (int)EnumCard.NormalDog;

        if (player == (int)EnumPlayer.Player)
        {
            Debug.Log("Player Took " + revealedCard + " and Removed " + playerHandCard[cardIndex] + " in " + cardIndex);
            cardToWaste = playerHandCard[cardIndex];
            playerHandCard[cardIndex] = revealedCard;
        }

        else if (player == (int)EnumPlayer.AI)
        {
            Debug.Log("AI Took " + revealedCard + " and Removed " + aiHandCard[cardIndex] + " in " + cardIndex);
            cardToWaste = aiHandCard[cardIndex];
            aiHandCard[cardIndex] = revealedCard;
        }

        revealedCard = cardToWaste;
    }

    public int NeedSheepCount(int player)
    {
        int count = 5;

        if (player == (int)EnumPlayer.Player)
        {
            for (int i = 0; i < playerHandCard.Count; i++)
            {
                if (playerHandCard[i] == (int)EnumCard.NormalSheep
                    || playerHandCard[i] == (int)EnumCard.GroupSheep
                    || playerHandCard[i] == (int)EnumCard.ShadowSheep
                    || playerHandCard[i] == (int)EnumCard.NormalDog)
                {
                    count--;
                }
            }
        }

        else if (player == (int)EnumPlayer.AI)
        {
            for (int i = 0; i < aiHandCard.Count; i++)
            {
                if (aiHandCard[i] == (int)EnumCard.NormalSheep
                    || aiHandCard[i] == (int)EnumCard.GroupSheep
                    || aiHandCard[i] == (int)EnumCard.ShadowSheep
                    || aiHandCard[i] == (int)EnumCard.NormalDog)
                {
                    count--;
                }
            }
        }

        return count;
    }

    public int NeedWolfCount(int player)
    {
        int count = 5;

        if (player == (int)EnumPlayer.Player)
        {
            for (int i = 0; i < playerHandCard.Count; i++)
            {
                if (playerHandCard[i] == (int)EnumCard.NormalWolf
                    || playerHandCard[i] == (int)EnumCard.GroupWolf
                    || playerHandCard[i] == (int)EnumCard.ShadowWolf
                    || playerHandCard[i] == (int)EnumCard.NormalDog)
                {
                    count--;
                }
            }
        }

        else if (player == (int)EnumPlayer.AI)
        {
            for (int i = 0; i < aiHandCard.Count; i++)
            {
                if (aiHandCard[i] == (int)EnumCard.NormalWolf
                    || aiHandCard[i] == (int)EnumCard.GroupWolf
                    || aiHandCard[i] == (int)EnumCard.ShadowWolf
                    || aiHandCard[i] == (int)EnumCard.NormalDog)
                {
                    count--;
                }
            }
        }

        return count;
    }
}

