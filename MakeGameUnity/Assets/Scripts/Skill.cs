using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public List<GameObject> Images = new List<GameObject>();
    public List<GameObject> Buttons = new List<GameObject>();
    public List<Image> ButtonsImages = new List<Image>();

    private float cooldown;

    private int Index;

    private bool OnOff = false;

    private void Start()
    {
        GameObject SkillsObj = GameObject.Find("Skills");

        for (int i = 0; i < SkillsObj.transform.childCount; ++i)
            Images.Add(SkillsObj.transform.GetChild(i).gameObject);

        for (int i = 0; i < Images.Count; ++i)
            Buttons.Add(Images[i].transform.GetChild(0).gameObject);

        for (int i = 0; i < Buttons.Count; ++i)
            ButtonsImages.Add(Buttons[i].GetComponent<Image>());

        cooldown = 0.0f;

        print("1");
    }

    public void PushButton()
    {
        ButtonsImages[Index].fillAmount = 0;
        Buttons[Index].GetComponent<Button>().enabled = false;

        switch(Index)
        {
            case 0 :
                StartCoroutine(Testcase1_Coroutine());
                break;
        }
    }

    IEnumerator Testcase1_Coroutine()
    {
        float cool = cooldown;
        int i = Index;
        if (OnOff)
        {
            while (ButtonsImages[i].fillAmount != 1)
            {
                ButtonsImages[i].fillAmount += Time.deltaTime * cooldown;
                yield return null;
            }

            Buttons[i].GetComponent<Button>().enabled = true;
            ControllerManager.GetInstance().BulletSpeed -= 0.5f;
            ControllerManager.GetInstance().BulletDamage -= 2;
            OnOff = false;

            print("3");
            print(ControllerManager.GetInstance().BulletSpeed + " / " + ControllerManager.GetInstance().BulletDamage);
        }
    }

    // cooldown이 클수록 쿨타임이 줄어듦
    public void Testcase()
    {
        Index = 0;

        cooldown = 0.5f;

        ControllerManager.GetInstance().BulletSpeed += 0.5f;
        ControllerManager.GetInstance().BulletDamage += 2;
        OnOff = true;

        print("2");
        print(ControllerManager.GetInstance().BulletSpeed + " / " + ControllerManager.GetInstance().BulletDamage);
    }

    public void Testcase1()
    {
        Index = 1;

        cooldown = 0.25f;


    }

    public void Testcase2()
    {
        Index = 2;

        cooldown = 0.5f;


    }

    public void Testcase3()
    {
        Index = 3;

        cooldown = 0.5f;


    }

    public void Testcase4()
    {
        Index = 4;

        cooldown = 0.5f;

        
    }

    public void Testcase5()
    {
        Index = 5;

        cooldown = 0.5f;


    }
}
