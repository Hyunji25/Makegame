using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarController : MonoBehaviour
{
    private Slider EXPBar;
    public int level;
    private GameObject LevelText;

    private void Awake()
    {
        EXPBar = GetComponent<Slider>();
        LevelText = GameObject.Find("Level");
    }
    private void Start()
    {
        EXPBar.maxValue = 10;
        EXPBar.value = ControllerManager.GetInstance().EXP;
        level = 0;
    }

    private int GetLevel()
    {
        return level;
    }

    private void Update(int level)
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
            LevelText.GetComponent<Text>().text = level.ToString();

            print(level);
            print(EXPBar.maxValue);
        }
    }

    internal static object GetInstance()
    {
        throw new NotImplementedException();
    }
}