using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandButtonScaler : MonoBehaviour
{
    const float defaultScreenWidth = 1920;
    const float defaultScreenHeight = 1080;

    float curScreenWidth = 1920;
    float curScreenHeight = 1080;
    float factorSizeW = 1;
    float factorSizeH = 1;

    public float defaultButtonWidth = 160;
    public float defaultButtonHeight = 220;

    float defaultPosX;
    float defaultPosY;


    float factorButtonWidth = 160;
    float factorButtonHeight = 220;
    float factorPosX;
    float factorPosY;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultPosX = rectTransform.anchoredPosition.x;
        defaultPosY = rectTransform.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        ScaleTransform();

    }

    void ScaleTransform()
    {
        curScreenWidth = Screen.width;
        curScreenHeight = Screen.height;

        factorSizeW = curScreenWidth / defaultScreenWidth;
        factorSizeH = curScreenHeight / defaultScreenHeight;

        factorButtonWidth = defaultButtonWidth * factorSizeW;
        factorButtonHeight = defaultButtonHeight * factorSizeW;

        factorPosX = defaultPosX * factorSizeW;
        factorPosY = defaultPosY * factorSizeH;

        rectTransform.sizeDelta = new Vector2(factorButtonWidth, factorButtonHeight);

        rectTransform.anchoredPosition = new Vector2(factorPosX, factorPosY);

    }
}
