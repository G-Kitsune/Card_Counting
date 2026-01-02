using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField]
    Slider Normal_Slider;
    [SerializeField]
    Slider Group_Slider;
    [SerializeField]
    Slider Shadow_Slider;
    [SerializeField]
    Slider Dog_Slider;

    [SerializeField]
    Text T_Normal_Count;
    [SerializeField]
    Text T_Group_Count;
    [SerializeField]
    Text T_Shadow_Count;
    [SerializeField]
    Text T_Dog_Count;
    
    void Update()
    {
        SetSlider();
    }

    void SetSlider()
    {
        T_Normal_Count.text = Convert.ToString((int)Normal_Slider.value);
        T_Group_Count.text = Convert.ToString((int)Group_Slider.value);
        T_Shadow_Count.text = Convert.ToString((int)Shadow_Slider.value);
        T_Dog_Count.text = Convert.ToString((int)Dog_Slider.value);
    }
}
