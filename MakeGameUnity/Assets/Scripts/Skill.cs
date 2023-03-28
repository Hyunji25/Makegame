using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public List<GameObject> Images = new List<GameObject>();
    public List<GameObject> Buttons = new List<GameObject>();
    public List<Image> ButtonsImages = new List<Image>();

    private float cooldown;

    private int Index;

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
    }

    public void PushButton()
    {
        ButtonsImages[Index].fillAmount = 0;
        Buttons[Index].GetComponent<Button>().enabled = false;

        StartCoroutine(Testcase1_Coroutine());
    }

    IEnumerator Testcase1_Coroutine()
    {
        float cool = cooldown;
        int i = Index;

        while (ButtonsImages[i].fillAmount != 1)
        {
            ButtonsImages[i].fillAmount += Time.deltaTime * cooldown;
            yield return null;
        }

        Buttons[i].GetComponent<Button>().enabled = true;

    }

    public void Testcase()
    {
        Index = 0;

        cooldown = 0.5f;

        ControllerManager.GetInstance().BulletSpeed += 0.025f;
        ControllerManager.GetInstance().BulletDamage++;

    }

    public void Testcase1()
    {
        Index = 1;

        cooldown = 0.5f;


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

        ControllerManager.GetInstance().Player_HP += 1;
    }
}
