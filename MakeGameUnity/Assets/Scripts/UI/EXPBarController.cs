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

// ���� ���� �߰��ؾ��ҵ�