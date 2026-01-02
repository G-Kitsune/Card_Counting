using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandActive : MonoBehaviour
{
    public int index = 0;

    public GameObject cardButton;

    public void AbleButton()
    {
        cardButton.SetActive(true);
    }

    public void DisableButton()
    {
        cardButton.SetActive(false);
    }
}
