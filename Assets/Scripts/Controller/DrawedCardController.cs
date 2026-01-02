using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawedCardController : CardController
{
    public GameObject cardManager;
    int cardCode = (int)EnumCard.NormalSheep;

    Coroutine runningCoroutine;
    
    void Update()
    {
        GetComponent<CardImageChanger>().cardCode = this.cardCode;
    }

    public void JumpDeck()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        
        cardCode = cardManager.GetComponent<GameCardManager>().revealedCard;
        
        Jump(deck);
    }

    public void JumpCenter()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        cardCode = cardManager.GetComponent<GameCardManager>().revealedCard;
        
        Jump(center);
    }

    public void JumpPlayerHand()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        cardCode = cardManager.GetComponent<GameCardManager>().revealedCard;

        Jump(playerHand);
    }

    public void JumpAIHand()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        cardCode = cardManager.GetComponent<GameCardManager>().revealedCard;

        Jump(aiHand);
    }

    public void JumpGrave()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        cardCode = cardManager.GetComponent<GameCardManager>().revealedCard;
        GetComponent<CardImageChanger>().cardShow = false;
        GetComponent<CardImageChanger>().cardCode = this.cardCode;
        Jump(grave);
    }

    public void Draw(int player)
    {
        if (runningCoroutine != null)
        {
            JumpDeck();
        }

        cardCode = cardManager.GetComponent<GameCardManager>().revealedCard;
        GetComponent<CardImageChanger>().cardShow = false;
        GetComponent<CardImageChanger>().cardCode = this.cardCode;

        runningCoroutine = StartCoroutine(Move(deck, center));
        
    }

    public void ToHand(int player)
    {

        if (runningCoroutine != null)
        {
            JumpCenter();
        }

        if (player == (int)EnumPlayer.Player)
        {
            runningCoroutine = StartCoroutine(Patrol(center, playerHand));
        }

        else
        {
            runningCoroutine = StartCoroutine(Patrol(center, aiHand));
        }

        cardCode = cardManager.GetComponent<GameCardManager>().revealedCard;
        GetComponent<CardImageChanger>().cardCode = this.cardCode;
    }

    public void ToGrave()
    {
        if (runningCoroutine != null)
        {
            JumpCenter();
        }

        GetComponent<CardImageChanger>().cardShow = false;
        cardCode = cardManager.GetComponent<GameCardManager>().revealedCard;

        runningCoroutine = StartCoroutine(Move(center, grave));
    }
}
