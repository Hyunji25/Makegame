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

    public void Testcase1() // ����
    {
        cooldown = 0.5f;
        index = 0;
        ControllerManager.GetInstance().BulletSpeed += 0.025f;
    }

    public void Testcase2() // ���
    {
        cooldown = 0.5f;
        index = 1;
        print("�׽�Ʈ �޼���2 �Դϴ�.");
    }

    public void Testcase3() // �̼�
    {
        cooldown = 0.5f;
        index = 2;
        ControllerManager.GetInstance().PlayerSpeed += 100.0f;
        print("�׽�Ʈ �޼���3 �Դϴ�.");
    }

    public void Testcase4() // ����
    {
        cooldown = 0.5f;
        index = 3;
        print("�׽�Ʈ �޼���4 �Դϴ�.");
    }

    public void Testcase5() // ȸ��
    {
        cooldown = 0.5f;
        index = 4;
        ++HP;
        print("�׽�Ʈ �޼���5 �Դϴ�.");
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


// �� ���� ������ �� �ٴ� �ź��� �ؾ��ҵ�