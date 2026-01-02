using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject playerTurnText;
    public GameObject aiTurnText;
    public GameObject additionalTurnText;
    public GameObject chooseText;

    public GameObject takeButton;
    public GameObject discardButton;

    public GameObject endBox;
    public GameObject restartButton;
    public GameObject toTitleButton;

    public Text turnText;
    public Text deckText;
   
    public Text toWinSheepText;
    public Text toWinWolfText;
    public Text endText;

    public bool decided = false;
    public bool takeOrDiscard = false;


    // Start is called before the first frame update
    void Start()
    {
        playerTurnText.SetActive(false);
        aiTurnText.SetActive(false);
        additionalTurnText.SetActive(false);
        chooseText.SetActive(false);

        takeButton.SetActive(false);
        discardButton.SetActive(false);
        endBox.SetActive(false);
        restartButton.SetActive(false);
        toTitleButton.SetActive(false);
    }

    void Update()
    {
        if (gameManager.GetComponent<TurnManager>().gameStart == true)
        {
            deckText.text = Convert.ToString("DECK " + gameManager.GetComponent<GameCardManager>().cardDeck.Count);
            turnText.text = Convert.ToString("TURN " + gameManager.GetComponent<TurnManager>().turnCount);
            NeedToWin();
        }
    }

    void NeedToWin()
    {
        toWinSheepText.text = Convert.ToString("Sheep: " + gameManager.GetComponent<GameCardManager>().NeedSheepCount((int)EnumPlayer.Player));
        toWinWolfText.text = Convert.ToString("or Wolf: " + gameManager.GetComponent<GameCardManager>().NeedWolfCount((int)EnumPlayer.Player));
    }

    public void TurnStart(int player, bool additionalTurn)
    {
        StartCoroutine(TurnStartText(player, additionalTurn));
    }

    public void AbleButtons()
    {
        
        takeButton.SetActive(true);
        discardButton.SetActive(true);
    }

    public bool DecideCheck()
    {
        if (this.decided == true)
        {
            takeButton.SetActive(false);
            discardButton.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Take()
    {
        takeOrDiscard = true;
        this.decided = true;
    }

    public void Discard()
    {
        takeOrDiscard = false;
        this.decided = true;
    }



    public void GameEnd(int player)
    {
        if (player == (int)EnumPlayer.Player)
        {
            endText.text = Convert.ToString("You Win!");
        }

        else
        {
            endText.text = Convert.ToString("You Lose!");
        }

        endBox.SetActive(true);
        restartButton.SetActive(true);
        toTitleButton.SetActive(true);
    }

    public IEnumerator TurnStartText(int player, bool additionalTurn)
    {
        decided = false;

        if (player == (int)EnumPlayer.Player)
        {
            if (additionalTurn == true)
            {
                additionalTurnText.SetActive(true);
            }
            playerTurnText.SetActive(true);

            yield return new WaitForSeconds(0.8f);

            playerTurnText.SetActive(false);
            additionalTurnText.SetActive(false);
        }

        else if (player == (int)EnumPlayer.AI)
        {
            if (additionalTurn == true)
            {
                additionalTurnText.SetActive(true);
            }
            aiTurnText.SetActive(true);

            yield return new WaitForSeconds(0.8f);

            aiTurnText.SetActive(false);
            additionalTurnText.SetActive(false);
        }
    }
}
