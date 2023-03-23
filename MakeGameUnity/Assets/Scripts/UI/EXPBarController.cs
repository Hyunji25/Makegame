using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarController : MonoBehaviour
{
    private Slider EXPBar;
    private int EXP; // ���� EXP
    private int MaxEXP; // �ִ� EXP
    private int Level; // ����

    private void Awake()
    {
        EXPBar = GetComponent<Slider>();
    }

    private void Start()
    {
        EXPBar.maxValue = ControllerManager.GetInstance().EXP_MAX;
        EXPBar.value = EXPBar.maxValue;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ControllerManager.GetInstance().EXP_MAX -= 1;
        }

        if (Input.GetMouseButton(1))
        {
            ControllerManager.GetInstance().EXP_MAX += 1;
        }

        EXPBar.value = ControllerManager.GetInstance().EXP_MAX;

        if (ControllerManager.GetInstance().EXP_MAX <= 0)
        {

        }
    }

    /*
    private void Start()
    {
        EXP = 0;
        MaxEXP = 5;
        Level = 1;
        EXPBar.maxValue = MaxEXP;
        EXPBar.value = EXPBar.maxValue;
    }

    private void Update()
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
     */
}

// ���� ���� �߰��ؾ��ҵ�