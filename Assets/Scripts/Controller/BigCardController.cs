using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCardController : MonoBehaviour
{
    const float defaultScreenWidth = 1920;
    const float defaultScreenHeight = 1080;

    public float defaultCardWidth = 320;
    public float defaultCardHeight = 440;

    float curScreenWidth = 1920;
    float curScreenHeight = 1080;

    float factorSizeW;
    float factorSizeH;

    public Vector2 scaledPos = new Vector2(0, 0);

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, defaultScreenHeight * 2);
    }

    void Update()
    {
        curScreenWidth = Screen.width;
        curScreenHeight = Screen.height;

        factorSizeW = curScreenWidth / defaultScreenWidth;
        factorSizeH = curScreenHeight / defaultScreenHeight;

        if (GetComponent<CardImageChanger>().cardShow == true)
        {
            rectTransform.anchoredPosition = scaledPos;
        }
        else
        {
            rectTransform.anchoredPosition = new Vector2(0, defaultScreenHeight * 2);
        }

        SizeSet();
    }

    public void PositionSet(float posX, float posY)
    {

        float factorPosX = posX * factorSizeW;
        float factorPosY = posY * factorSizeH;

        Vector3 scaledPos = Camera.main.WorldToScreenPoint(new Vector3(factorPosX, factorPosY, 0));

        this.scaledPos.x = scaledPos.x;
        this.scaledPos.y = scaledPos.y;
    }

    void SizeSet()
    {
        float factorCardWidth = defaultCardWidth * factorSizeW;
        float factorCardHeight = defaultCardHeight * factorSizeW;

        rectTransform.sizeDelta = new Vector2(factorCardWidth, factorCardHeight);
    }
}
