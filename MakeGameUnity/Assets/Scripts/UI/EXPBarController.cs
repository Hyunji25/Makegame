using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarController : MonoBehaviour
{
    private Slider EXPBar;
    private int EXP; // 현재 EXP
    private int MaxEXP; // 최대 EXP
    private int Level; // 레벨

    private void Awake()
    {
        EXPBar = GetComponent<Slider>();
    }

    void Start()
    {
        EXP = 0;
        MaxEXP = 5;
        Level = 1;
        EXPBar.maxValue = MaxEXP;
        EXPBar.value = EXPBar.maxValue;
    }

    void Update()
    {
        if (Input.GetKeyDown("a")) 
        {
            ++EXP;
        }

        EXPBar.value = EXP;

        if (EXP >= MaxEXP)
        {
            EXP -= MaxEXP;
            ++Level;
            MaxEXP += 5;
            print(EXP);
            print(Level);
        }
    }
}

// 레벨 숫자 추가해야할듯