using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigCardImageChanger : CardImageChanger
{
    void Start()
    {
        cardShow = false;
    }

    void Update()
    {
        ImageChanger();
    }

    public void ImageChanger()
    {
        if (cardShow == true)
        {
            switch (cardCode)
            {
                case (int)EnumCard.NormalSheep:
                    GetComponent<Image>().sprite = normalSheep;
                    break;

                case (int)EnumCard.NormalWolf:
                    GetComponent<Image>().sprite = normalWolf;
                    break;

                case (int)EnumCard.GroupSheep:
                    GetComponent<Image>().sprite = groupSheep;
                    break;

                case (int)EnumCard.GroupWolf:
                    GetComponent<Image>().sprite = groupWolf;
                    break;

                case (int)EnumCard.ShadowSheep:
                    GetComponent<Image>().sprite = shadowSheep;
                    break;

                case (int)EnumCard.ShadowWolf:
                    GetComponent<Image>().sprite = shadowWolf;
                    break;

                case (int)EnumCard.NormalDog:
                    GetComponent<Image>().sprite = normalDog;
                    break;

                default:
                    GetComponent<Image>().sprite = cardBack;
                    break;
            }
        }

        else
        {
            GetComponent<Image>().sprite = cardBack;
        }
    }
}
