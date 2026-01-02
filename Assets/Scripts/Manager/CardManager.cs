using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardManager : MonoBehaviour
{
    [SerializeField]
    Slider Normal_Slider;
    [SerializeField]
    Slider Group_Slider;
    [SerializeField]
    Slider Shadow_Slider;
    [SerializeField]
    Slider Dog_Slider;

    void Start()
    {
        FillOnNullData();
        SetSlider();
    }

    public void ResetCount()
    {
        PlayerPrefs.SetInt("NormalCount", 10);
        PlayerPrefs.SetInt("GroupCount", 2);
        PlayerPrefs.SetInt("ShadowCount", 2);
        PlayerPrefs.SetInt("DogCount", 2);
        SetSlider();
    }

    void FillOnNullData()
    {
        if (PlayerPrefs.GetInt("NormalCount") == null)
        {
            PlayerPrefs.SetInt("NormalCount", 10);
        }
        if (PlayerPrefs.GetInt("GroupCount") == null)
        {
            PlayerPrefs.SetInt("GroupCount", 2);
        }
        if (PlayerPrefs.GetInt("ShadowCount") == null)
        {
            PlayerPrefs.SetInt("ShadowCount", 2);
        }
        if (PlayerPrefs.GetInt("DogCount") == null)
        {
            PlayerPrefs.SetInt("DogCount", 2);
        }
    }

    public void SetNormalCount()
    {
        PlayerPrefs.SetInt("NormalCount", (int)Normal_Slider.value);
    }

    public void SetGroupCount()
    {
        PlayerPrefs.SetInt("GroupCount", (int)Group_Slider.value);
    }

    public void SetShadowCount()
    {
        PlayerPrefs.SetInt("ShadowCount", (int)Shadow_Slider.value);
    }

    public void SetDogCount()
    {
        PlayerPrefs.SetInt("DogCount", (int)Dog_Slider.value);
    }

    void SetSlider()
    {
        if (PlayerPrefs.GetInt("NormalCount") != null)
        {
            Normal_Slider.value = PlayerPrefs.GetInt("NormalCount");
        }
        if (PlayerPrefs.GetInt("GroupCount") != null)
        {
            Group_Slider.value = PlayerPrefs.GetInt("GroupCount");
        }
        if (PlayerPrefs.GetInt("ShadowCount") != null)
        {
            Shadow_Slider.value = PlayerPrefs.GetInt("ShadowCount");
        }
        if (PlayerPrefs.GetInt("DogCount") != null)
        {
            Dog_Slider.value = PlayerPrefs.GetInt("DogCount");
        }
    }
}