using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckImageController : MonoBehaviour
{
    public GameObject cardManager;

    void Update()
    {
        if (cardManager.GetComponent<GameCardManager>().cardDeck.Count > 0)
        {
            gameObject.SetActive(true);
        }
        /*
        else
        {
            gameObject.SetActive(false);
        }
        */
    }
}
