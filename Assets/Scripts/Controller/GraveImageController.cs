using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveImageController : MonoBehaviour
{
    public GameObject cardManager;

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(cardManager.GetComponent<TurnManager>().cardIsDiscarded);
    }
}
