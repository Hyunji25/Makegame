using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarController : MonoBehaviour
{
    private Slider EXPBar;

    private void Awake()
    {
        EXPBar = GetComponent<Slider>();
    }

    void Start()
    {
        EXPBar.maxValue = 10;
        EXPBar.value = EXPBar.maxValue;
    }

    void Update()
    {
        if (Input.GetKeyDown("a")) 
        {

        }
    }
}

// ���� ���� �߰��ؾ��ҵ�