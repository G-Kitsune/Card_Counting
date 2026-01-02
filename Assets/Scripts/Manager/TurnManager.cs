using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turnCount = 0;
    public int turnPhase = (int)EnumPhase.Draw;
    public int turnPlayer = (int)EnumPlayer.Player;

    public int takenCard = 0;
    public int chooseIndex = -1;

    int defaultDeckCount = 0;

    float infiniteWaitTime = 99999999f;

    bool takeOrDiscard = false;
    bool additionalTurn = false;
    public bool gameStart = false;
    public bool cardIsDiscarded = false;

    public GameObject UIManager;
    public GameObject playerHand1;
    public GameObject playerHand2;
    public GameObject playerHand3;
    public GameObject playerHand4;
    public GameObject playerHand5;
    public GameObject aiHand1;
    public GameObject aiHand2;
    public GameObject aiHand3;
    public GameObject aiHand4;
    public GameObject aiHand5;
    public GameObject gameCard;


    void Start()
    {
        takenCard = 0;
        chooseIndex = -1;

        turnCount = 0;
        turnPhase = (int)EnumPhase.Draw;
        turnPlayer = (int)EnumPlayer.Player;

        gameStart = false;
        takeOrDiscard = false;
        additionalTurn = false;
        cardIsDiscarded = false;
    }
    
    void Update()
    {
        if (gameStart == true)
        {

            if (CheckGameEnd() == (int)EnumGameEnd.PlayerWin)
            {
                Debug.Log("Player Win");
                StartCoroutine(PlayerWin());
                gameStart = false;
            }

            else if (CheckGameEnd() == (int)EnumGameEnd.AiWin)
            {
                Debug.Log("AI Win");
                StartCoroutine(AiWin());
                gameStart = false;
            }

            else
            {
                /*
                aiHand1.GetComponent<CardImageChanger>().cardShow = true;
                aiHand2.GetComponent<CardImageChanger>().cardShow = true;
                aiHand3.GetComponent<CardImageChanger>().cardShow = true;
                aiHand4.GetComponent<CardImageChanger>().cardShow = true;
                aiHand5.GetComponent<CardImageChanger>().cardShow = true;
                */

                switch (turnPhase)
                {
                    case (int)EnumPhase.Draw:
                        StartCoroutine(PhaseDraw());
                        break;

                    case (int)EnumPhase.TakeOrDiscard:
                        StartCoroutine(PhaseTakeOrDiscard());
                        break;

                    case (int)EnumPhase.TakeEffect:
                        StartCoroutine(PhaseTakeEffect());
                        break;

                    case (int)EnumPhase.RecycleCard:
                        StartCoroutine(PhaseRecycleCard());
                        break;

                    case (int)EnumPhase.EndTurn:
                        StartCoroutine(PhaseEnd());
                        break;

                    case (int)EnumPhase.AdditionalTurn:
                        StartCoroutine(AdditionalTurn());
                        break;

                    default:
                        turnPhase = (int)EnumPhase.Draw;
                        break;
                }
            }
        }

        else if (CheckGameEnd() == (int)EnumGameEnd.NotYet)
        {
            GameStart();
        }
    }

    int CheckGameEnd()
    {
        if (GetComponent<GameCardManager>().NeedSheepCount((int)EnumPlayer.Player) == 0
            || GetComponent<GameCardManager>().NeedWolfCount((int)EnumPlayer.Player) == 0)
        {
            return (int)EnumGameEnd.PlayerWin;
        }
        
        else if (GetComponent<GameCardManager>().NeedSheepCount((int)EnumPlayer.AI) == 0
            || GetComponent<GameCardManager>().NeedWolfCount((int)EnumPlayer.AI) == 0)
        {
            return (int)EnumGameEnd.AiWin;
        }

        return (int)EnumGameEnd.NotYet;
    }

    IEnumerator PlayerWin()
    {
        aiHand1.GetComponent<CardImageChanger>().cardShow = true;
        aiHand2.GetComponent<CardImageChanger>().cardShow = true;
        aiHand3.GetComponent<CardImageChanger>().cardShow = true;
        aiHand4.GetComponent<CardImageChanger>().cardShow = true;
        aiHand5.GetComponent<CardImageChanger>().cardShow = true;

        UIManager.GetComponent<GameUIManager>().GameEnd((int)EnumPlayer.Player);

        while (true)
        {
            yield return new WaitForSeconds(infiniteWaitTime);
        }
    }

    IEnumerator AiWin()
    {
        aiHand1.GetComponent<CardImageChanger>().cardShow = true;
        aiHand2.GetComponent<CardImageChanger>().cardShow = true;
        aiHand3.GetComponent<CardImageChanger>().cardShow = true;
        aiHand4.GetComponent<CardImageChanger>().cardShow = true;
        aiHand5.GetComponent<CardImageChanger>().cardShow = true;

        yield return new WaitForSeconds(2.0f);

        UIManager.GetComponent<GameUIManager>().GameEnd((int)EnumPlayer.AI);

        while (true)
        {
            yield return new WaitForSeconds(infiniteWaitTime);
        }
    }

    void GameStart()
    {
        DisableButtons();

        GetComponent<GameCardManager>().FillOnNullData();

        GetComponent<GameCardManager>().LoadData();

        GetComponent<GameCardManager>().ShuffleDeck();

        for (int i = 0; i < 5; i++)
        {
            GetComponent<GameCardManager>().FirstDrawCard((int)EnumPlayer.Player);
            GetComponent<GameCardManager>().FirstDrawCard((int)EnumPlayer.AI);
        }

        defaultDeckCount = GetComponent<GameCardManager>().cardDeck.Count;

        gameStart = true;
    }

    IEnumerator PhaseDraw()
    {
        while (gameCard.transform.position.x < gameCard.GetComponent<DrawedCardController>().grave.x && turnCount > 0)
        {
            yield return new WaitForSeconds(infiniteWaitTime);
        }

        //카드 이동이 끝날 때까지 대기

        takenCard = (int)EnumCard.NormalDog;

        turnCount += 1;

        Debug.Log("Draw Phase " + turnCount);

        if (GetComponent<GameCardManager>().cardDeck.Count <= 0)
        {
            cardIsDiscarded = false;
            GetComponent<GameCardManager>().ShuffleDeck();
        }

        GetComponent<GameCardManager>().DrawCard(turnPlayer);

        UIManager.GetComponent<GameUIManager>().TurnStart(turnPlayer, additionalTurn);

        gameCard.GetComponent<DrawedCardController>().Draw(turnPlayer);

        turnPhase = (int)EnumPhase.TakeOrDiscard;
        chooseIndex = -1;

        additionalTurn = false;

        yield return null;
    }

    IEnumerator PhaseTakeOrDiscard()
    {

        while (gameCard.transform.position.x < gameCard.GetComponent<DrawedCardController>().center.x)
        {
            yield return new WaitForSeconds(infiniteWaitTime);
        }

        if (turnPlayer == (int)EnumPlayer.Player)
        {
            gameCard.GetComponent<CardImageChanger>().cardShow = true;
        }
        else
        {
            gameCard.GetComponent<CardImageChanger>().cardShow = false;
        }

        //카드 이동이 끝날 때까지 대기

        Debug.Log("Take or Discard Phase " + turnCount);

        if (turnPlayer == (int)EnumPlayer.Player)
        {
            takeOrDiscard = false;

            UIManager.GetComponent<GameUIManager>().AbleButtons();

            while (UIManager.GetComponent<GameUIManager>().DecideCheck() != true)
            {
                yield return new WaitForSeconds(infiniteWaitTime);
            }

            this.takeOrDiscard = UIManager.GetComponent<GameUIManager>().takeOrDiscard;


            if (takeOrDiscard == true)
            {
                UIManager.GetComponent<GameUIManager>().chooseText.SetActive(true);
                this.AbleButtons();

                while (ChooseRemoveCard() != true)
                {
                    yield return new WaitForSeconds(infiniteWaitTime);
                }

                gameCard.GetComponent<DrawedCardController>().ToHand((int)EnumPlayer.Player);

            }
        }
        else
        {
            chooseIndex = AIChooseRemoveCard();

            AICardHide(chooseIndex);

            Debug.Log("AI Choose " + chooseIndex + " to Waste");
                
            gameCard.GetComponent<DrawedCardController>().ToHand((int)EnumPlayer.AI);

            
        }

        turnPhase = (int)EnumPhase.TakeEffect;
        yield return null;
    }

    IEnumerator PhaseTakeEffect()
    {

        while (gameCard.GetComponent<DrawedCardController>().patrolEnded == false && chooseIndex != -1)
        {
            yield return new WaitForSeconds(infiniteWaitTime);
        }

        if (takeOrDiscard == true && turnPlayer == (int)EnumPlayer.Player && chooseIndex != -1)
        {
            GetComponent<GameCardManager>().ExchangeCard((int)EnumPlayer.Player, chooseIndex);
            takenCard = GetComponent<GameCardManager>().playerHandCard[chooseIndex];
            chooseIndex = -1;
        }

        if (turnPlayer == (int)EnumPlayer.AI && chooseIndex != -1)
        {
            GetComponent<GameCardManager>().ExchangeCard((int)EnumPlayer.AI, chooseIndex);
            takenCard = GetComponent<GameCardManager>().aiHandCard[chooseIndex];
            chooseIndex = -1;
        }

        gameCard.GetComponent<DrawedCardController>().patrolEnded = false;

        //카드 이동이 끝날 때까지 대기

        switch (takenCard)
        {
            case (int)EnumCard.GroupSheep:
            case (int)EnumCard.GroupWolf:
                Debug.Log("Additional Turn " + turnCount);

                turnPhase = (int)EnumPhase.AdditionalTurn;
                break;
                //추가 턴 처리

            case (int)EnumCard.ShadowSheep:
            case (int)EnumCard.ShadowWolf:
                Debug.Log("Reveal Card " + turnCount);
                if (turnPlayer == (int)EnumPlayer.Player)
                {
                    AICardReveal();
                }
                turnPhase = (int)EnumPhase.RecycleCard;
                break;
                //카드 공개 처리

            default:
                Debug.Log("Normal Card " + turnCount);
                turnPhase = (int)EnumPhase.RecycleCard;
                break;
        }

        yield return null;
    }

    IEnumerator PhaseRecycleCard()
    {
        gameCard.GetComponent<DrawedCardController>().JumpCenter();

        Debug.Log("Recycle Phase " + turnCount);

        if (turnPlayer == (int)EnumPlayer.AI)
        {
            gameCard.GetComponent<CardImageChanger>().cardShow = true;

            takeOrDiscard = false;

            UIManager.GetComponent<GameUIManager>().AbleButtons();

            while (UIManager.GetComponent<GameUIManager>().DecideCheck() != true)
            {
                yield return new WaitForSeconds(infiniteWaitTime);
            }

            this.takeOrDiscard = UIManager.GetComponent<GameUIManager>().takeOrDiscard;


            if (takeOrDiscard == true)
            {
                UIManager.GetComponent<GameUIManager>().chooseText.SetActive(true);
                this.AbleButtons();

                while (ChooseRemoveCard() != true)
                {
                    yield return new WaitForSeconds(infiniteWaitTime);
                }

                gameCard.GetComponent<DrawedCardController>().ToHand((int)EnumPlayer.Player);

            }
        }

        else
        {

            chooseIndex = AIChooseRemoveCard();

            Debug.Log("AI Choose " + chooseIndex + " to Waste");

            gameCard.GetComponent<DrawedCardController>().ToHand((int)EnumPlayer.AI);

            AICardHide(chooseIndex);

        }

        turnPhase = (int)EnumPhase.EndTurn;

        yield return null;
    }

    IEnumerator PhaseEnd()
    {

        while (gameCard.GetComponent<DrawedCardController>().patrolEnded == false && chooseIndex != -1)
        {
            yield return new WaitForSeconds(infiniteWaitTime);
        }

        if (takeOrDiscard == true && turnPlayer == (int)EnumPlayer.AI && chooseIndex != -1)
        {
            GetComponent<GameCardManager>().ExchangeCard((int)EnumPlayer.Player, chooseIndex);
        }

        if (turnPlayer == (int)EnumPlayer.Player && chooseIndex != -1)
        {
            GetComponent<GameCardManager>().ExchangeCard((int)EnumPlayer.AI, chooseIndex);
        }

        gameCard.GetComponent<DrawedCardController>().patrolEnded = false;

        //카드 이동이 끝날 때까지 대기

        gameCard.GetComponent<DrawedCardController>().ToGrave();

        cardIsDiscarded = true;

        if (additionalTurn == false)
        {
            if (turnPlayer == (int)EnumPlayer.Player)
            {
                Debug.Log("Player Turn End " + turnCount);
                turnPlayer = (int)EnumPlayer.AI;
            }

            else
            {
                Debug.Log("AI Turn End " + turnCount);
                turnPlayer = (int)EnumPlayer.Player;
            }
        }


        turnPhase = (int)EnumPhase.Draw;

        yield return null;
    }

    IEnumerator AdditionalTurn()
    {
        additionalTurn = true;

        turnPhase = (int)EnumPhase.EndTurn;

        yield return null;
    }

    void AICardReveal()
    {
        int revealIndex = Random.Range(0, 4);

        if (aiHand1.GetComponent<CardImageChanger>().cardShow == true
            && aiHand2.GetComponent<CardImageChanger>().cardShow == true
            && aiHand3.GetComponent<CardImageChanger>().cardShow == true
            && aiHand4.GetComponent<CardImageChanger>().cardShow == true
            && aiHand5.GetComponent<CardImageChanger>().cardShow == true)
        {
            return;
        }

        switch (revealIndex)
        {
            case 0:

                if (aiHand1.GetComponent<CardImageChanger>().cardShow == true)
                {
                    AICardReveal();
                }
                else
                {
                    aiHand1.GetComponent<CardImageChanger>().cardShow = true;
                }
                
                break;

            case 1:

                if (aiHand2.GetComponent<CardImageChanger>().cardShow == true)
                {
                    AICardReveal();
                }
                else
                {
                    aiHand2.GetComponent<CardImageChanger>().cardShow = true;
                }

                break;

            case 2:

                if (aiHand3.GetComponent<CardImageChanger>().cardShow == true)
                {
                    AICardReveal();
                }
                else
                {
                    aiHand3.GetComponent<CardImageChanger>().cardShow = true;
                }

                break;

            case 3:

                if (aiHand4.GetComponent<CardImageChanger>().cardShow == true)
                {
                    AICardReveal();
                }
                else
                {
                    aiHand4.GetComponent<CardImageChanger>().cardShow = true;
                }

                break;

            case 4:

                if (aiHand5.GetComponent<CardImageChanger>().cardShow == true)
                {
                    AICardReveal();
                }
                else
                {
                    aiHand5.GetComponent<CardImageChanger>().cardShow = true;
                }

                break;

            default:

                if (aiHand1.GetComponent<CardImageChanger>().cardShow == true)
                {
                    AICardReveal();
                }
                else
                {
                    aiHand1.GetComponent<CardImageChanger>().cardShow = true;
                }

                break;
        }
    }

    void AICardHide(int revealIndex)
    {
        switch (revealIndex)
        {
            case 0:
                aiHand1.GetComponent<CardImageChanger>().cardShow = false;
                break;

            case 1:
                aiHand2.GetComponent<CardImageChanger>().cardShow = false;
                break;

            case 2:
                aiHand3.GetComponent<CardImageChanger>().cardShow = false;
                break;

            case 3:
                aiHand4.GetComponent<CardImageChanger>().cardShow = false;
                break;

            case 4:
                aiHand5.GetComponent<CardImageChanger>().cardShow = false;
                break;

            default:
                aiHand1.GetComponent<CardImageChanger>().cardShow = false;
                break;
        }
    }

    int AIChooseRemoveCard()
    {
        int aiSheep = 5 - GetComponent<GameCardManager>().NeedSheepCount((int)EnumPlayer.AI);
        int aiWolf = 5 - GetComponent<GameCardManager>().NeedWolfCount((int)EnumPlayer.AI);
        int chosenIndex = Random.Range(0, 4);

        if (aiSheep > aiWolf)
        {
            chosenIndex = GetComponent<GameCardManager>().aiHandCard.FindIndex(card => FindPureWolf(card)); //개가 아닌, 늑대 찾기
            Debug.Log("AI is Finding Pure Wolf");
        }

        else if (aiSheep < aiWolf)
        {
            chosenIndex = GetComponent<GameCardManager>().aiHandCard.FindIndex(card => FindPureSheep(card)); //개가 아닌, 양 찾기
            Debug.Log("AI is Finding Pure Sheep");
        }

        else
        {
            chosenIndex = GetComponent<GameCardManager>().aiHandCard.FindIndex(card => FindPureSheep(card) || FindPureWolf(card));//개가 아닌, 카드 찾기
            Debug.Log("AI is Finding a Card, not Dog");
        }

        return chosenIndex;
    }

    bool FindPureSheep(int inputCard)
    {
        if (inputCard == (int)EnumCard.NormalSheep || inputCard == (int)EnumCard.GroupSheep || inputCard == (int)EnumCard.ShadowSheep)

        {
            return true;
        }    

        else
        {
            return false;
        }
    }

    bool FindPureWolf(int inputCard)
    {
        if (inputCard == (int)EnumCard.NormalWolf || inputCard == (int)EnumCard.GroupWolf || inputCard == (int)EnumCard.ShadowWolf)

        {
            return true;
        }

        else
        {
            return false;
        }
    }

    bool ChooseRemoveCard()
    {
        if (chooseIndex != -1)
        {
            UIManager.GetComponent<GameUIManager>().chooseText.SetActive(false);
            DisableButtons();

            return true;
        }

        else
        {
            return false;
        }
    }

    void AbleButtons()
    {
        playerHand1.GetComponent<HandActive>().AbleButton();
        playerHand2.GetComponent<HandActive>().AbleButton();
        playerHand3.GetComponent<HandActive>().AbleButton();
        playerHand4.GetComponent<HandActive>().AbleButton();
        playerHand5.GetComponent<HandActive>().AbleButton();
    }

    void DisableButtons()
    {
        playerHand1.GetComponent<HandActive>().DisableButton();
        playerHand2.GetComponent<HandActive>().DisableButton();
        playerHand3.GetComponent<HandActive>().DisableButton();
        playerHand4.GetComponent<HandActive>().DisableButton();
        playerHand5.GetComponent<HandActive>().DisableButton();
    }
}