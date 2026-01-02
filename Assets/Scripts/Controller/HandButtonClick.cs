using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandButtonClick : MonoBehaviour
{
    public int cardIndex;

    public GameObject gameManager;

    public void IndexReturn()
    {
        gameManager.GetComponent<TurnManager>().chooseIndex = this.cardIndex;
    }
}
