using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardImageChanger : MonoBehaviour
{
    public bool cardShow;
    public int cardCode;

    public Sprite cardBack;

    public Sprite normalSheep;
    public Sprite normalWolf;

    public Sprite groupSheep;
    public Sprite groupWolf;

    public Sprite shadowSheep;
    public Sprite shadowWolf;

    public Sprite normalDog;

    void Start()
    {
        cardShow = false;
        cardCode = (int)EnumCard.Back;
    } 

    // Update is called once per frame
    void Update()
    {
        ImageChange();
    }

    public void ImageChange() 
    {

        if (cardShow == true)
        {
            
            switch (cardCode)
            {
                case (int)EnumCard.NormalSheep:
                    Debug.Log("ToNormalSheep");
                    GetComponent<SpriteRenderer>().sprite = normalSheep;
                    break;

                case (int)EnumCard.NormalWolf:
                    Debug.Log("ToNormalWolf");
                    GetComponent<SpriteRenderer>().sprite = normalWolf;
                    break;

                case (int)EnumCard.GroupSheep:
                    Debug.Log("ToGroupSheep");
                    GetComponent<SpriteRenderer>().sprite = groupSheep;
                    break;

                case (int)EnumCard.GroupWolf:
                    Debug.Log("ToGroupWolf");
                    GetComponent<SpriteRenderer>().sprite = groupWolf;
                    break;

                case (int)EnumCard.ShadowSheep:
                    Debug.Log("ToShadowSheep");
                    GetComponent<SpriteRenderer>().sprite = shadowSheep;
                    break;

                case (int)EnumCard.ShadowWolf:
                    Debug.Log("ToShadowWolf");
                    GetComponent<SpriteRenderer>().sprite = shadowWolf;
                    break;

                case (int)EnumCard.NormalDog:
                    Debug.Log("ToNormalDog");
                    GetComponent<SpriteRenderer>().sprite = normalDog;
                    break;

                default:
                    GetComponent<SpriteRenderer>().sprite = cardBack;
                    break;
            }
            
        }

        else
        {
            GetComponent<SpriteRenderer>().sprite = cardBack;
        }
    }
}
