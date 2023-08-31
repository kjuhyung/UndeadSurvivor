using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;
    // ������ ����, �������� ������ type ����

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:

                break;
            case InfoType.Level:

                break;
            case InfoType.Kill:
                break;

            case InfoType.Time:

                break;
            case InfoType.Health:

                break;
        }
    }
}
