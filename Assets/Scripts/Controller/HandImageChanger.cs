using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandImageChanger : CardImageChanger
{
    public int player;
    public int handIndex;

    public GameObject cardManager;

    // Start is called before the first frame update
    void Start()
    {
        cardCode = FindCode(player, handIndex);
        ImageChange();
    }

    // Update is called once per frame
    void Update()
    {
        cardCode = FindCode(player, handIndex);
        ImageChange();
    }

    int FindCode(int player, int index)
    {
        int image = 0;

        if (player == (int)EnumPlayer.Player)
        {
            try
            {
                image = cardManager.GetComponent<GameCardManager>().playerHandCard[index];
            }
            catch (Exception e)
            {
                Debug.Log(player + " " + index);
                Debug.Log(e);
            }
        }
        else
        {
            try
            {
                image = cardManager.GetComponent<GameCardManager>().aiHandCard[index];
            }
            catch (Exception e)
            {
                Debug.Log(player + " " + index);
                Debug.Log(e);
            }
        }

        return image;
    }
}
