using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class EXPBarController : MonoBehaviour
{
    private Slider EXPBar;
    public int level;
    private UnityEngine.UI.Text LevelText;

    private void Awake()
    {
        EXPBar = GetComponent<Slider>();
    }
    private void Start()
    {
        EXPBar.maxValue = 10;
        EXPBar.value = ControllerManager.GetInstance().EXP;
        level = 0;
        LevelText.text = "0";
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            ControllerManager.GetInstance().EXP += 5;
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            if (ControllerManager.GetInstance().EXP <= 0)
                ControllerManager.GetInstance().EXP += 0;
            else
                ControllerManager.GetInstance().EXP -= 5;
        }

        EXPBar.value = ControllerManager.GetInstance().EXP;

        if (ControllerManager.GetInstance().EXP >= EXPBar.maxValue)
        {
            ++level;
            ControllerManager.GetInstance().EXP = 0;
            EXPBar.maxValue += 5;
            LevelText.text = "1";

            print(level);
            print(EXPBar.maxValue);
        }
    }

    internal static object GetInstance()
    {
        throw new NotImplementedException();
    }
}