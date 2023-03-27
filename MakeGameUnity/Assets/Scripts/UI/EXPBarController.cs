using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarController : MonoBehaviour
{
    private Slider EXPBar;
    private int Level;

    private void Awake()
    {
        EXPBar = GetComponent<Slider>();
    }
    private void Start()
    {
        EXPBar.maxValue = 10;
        EXPBar.value = ControllerManager.GetInstance().EXP;
        Level = 0;
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
            ++Level;
            ControllerManager.GetInstance().EXP = 0;
            EXPBar.maxValue += 5;

            print(Level);
            print(EXPBar.maxValue);
        }
    }
}