using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private List<GameObject> Image = new List<GameObject>();
    private List<GameObject> Buttons = new List<GameObject>();
    private List<Image> ButtonImages = new List<Image>();
    private float cooldown;
    private int index;
    public int HP;

    private void Start()
    {
        GameObject SkillObj = GameObject.Find("Skills");

        for (int i = 0; i < SkillObj.transform.childCount; ++i)
            Image.Add(SkillObj.transform.GetChild(i).gameObject);

        for (int i = 0; i < Image.Count; ++i)
            Buttons.Add(Image[i].transform.GetChild(0).gameObject);

        for (int i = 0; i < Image.Count; ++i)
            ButtonImages.Add(Buttons[i].GetComponent<Image>());

        cooldown = 0.0f;
        index = 0;
    }

    public void PushButton()
    {
        ButtonImages[index].fillAmount = 0;
        Buttons[index].GetComponent<Button>().enabled = false;

        StartCoroutine(PushButton_Coroutine());
    }

    public void Testcase1() // 공속
    {
        cooldown = 0.5f;
        index = 0;
        ControllerManager.GetInstance().BulletSpeed += 0.025f;
    }

    public void Testcase2() // 방어
    {
        cooldown = 0.5f;
        index = 1;
        print("테스트 메세지2 입니다.");
    }

    public void Testcase3() // 이속
    {
        cooldown = 0.5f;
        index = 2;
        ControllerManager.GetInstance().PlayerSpeed += 100.0f;
        print("테스트 메세지3 입니다.");
    }

    public void Testcase4() // 무적
    {
        cooldown = 0.5f;
        index = 3;
        print("테스트 메세지4 입니다.");
    }

    public void Testcase5() // 회복
    {
        cooldown = 0.5f;
        index = 4;
        ++HP;
        print("테스트 메세지5 입니다.");
    }

    IEnumerator PushButton_Coroutine()
    {
        float cool = cooldown;
        int idx = index;
        while (ButtonImages[idx].fillAmount != 1)
        {
            ButtonImages[idx].fillAmount += Time.deltaTime * cool;
            yield return null;
        }
        Buttons[idx].GetComponent<Button>().enabled = true;
    }
}


// 적 공격 맞으면 피 다는 거부터 해야할듯