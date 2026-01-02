using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardExtensioner : HandImageChanger
{

    public GameObject cardBigger;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnMouseEnter()
    {
        if (GetComponent<CardImageChanger>().cardShow == true)
        {
            cardBigger.GetComponent<BigCardController>().PositionSet(transform.position.x, -transform.position.y / 10);

            cardBigger.GetComponent<BigCardImageChanger>().cardShow = true;
            cardBigger.GetComponent<BigCardImageChanger>().cardCode = GetComponent<CardImageChanger>().cardCode;
        }
    }

    void OnMouseExit()
    {
        cardBigger.GetComponent<BigCardImageChanger>().cardShow = false;
    }
}
