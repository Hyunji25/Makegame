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

    private BulletPattern bulletPattern;

    private GameObject skill;

    private void Awake()
    {
        skill = Resources.Load("Prefabs/PatternBullet") as GameObject;
    }


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

        switch(Index)
        {
            case 0 :
                StartCoroutine(Testcase_Coroutine());
                break;
            case 1:
                StartCoroutine(Testcase1_Coroutine());
                break;
            case 2:
                StartCoroutine(Testcase2_Coroutine());
                break;
            case 3:
                StartCoroutine(Testcase3_Coroutine());
                break;
        }
    }

    IEnumerator Testcase_Coroutine()
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
        }
    }

    // cooldown이 클수록 쿨타임이 줄어듦
    public void Testcase() // 플레이어 공격력 상승
    {
        Index = 0;

        cooldown = 0.05f;

        ControllerManager.GetInstance().BulletSpeed += 0.5f;
        ControllerManager.GetInstance().BulletDamage += 2;
        OnOff = true;
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
            ControllerManager.GetInstance().EnemyDamage += 2;
            ControllerManager.GetInstance().BossDamage += 2;
            ControllerManager.GetInstance().BossThrow += 1;
            OnOff = false;
        }
    }

    public void Testcase1() // 적과 보스 공격력 감소
    {
        Index = 1;

        cooldown = 0.25f;

        ControllerManager.GetInstance().EnemyDamage -= 2;
        ControllerManager.GetInstance().BossDamage -= 2;
        ControllerManager.GetInstance().BossThrow -= 1;
        OnOff = true;
    }

    IEnumerator Testcase2_Coroutine()
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
            OnOff = false;
        }
    }

    public void Testcase2() // 플레이어 체력 회복
    {
        Index = 2;

        cooldown = 0.5f;

        ControllerManager.GetInstance().Player_HP += 30;
        OnOff = true;
    }

    IEnumerator Testcase3_Coroutine()
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
            ControllerManager.GetInstance().EnemyDamage = 3;
            ControllerManager.GetInstance().BossDamage = 5;
            ControllerManager.GetInstance().BossThrow = 2;
            OnOff = false;
        }
    }

    public void Testcase3() // 무적, 적과 보스 공격력 0
    {
        Index = 3;

        cooldown = 0.5f;

        ControllerManager.GetInstance().EnemyDamage = 0;
        ControllerManager.GetInstance().BossDamage = 0;
        ControllerManager.GetInstance().BossThrow = 0;
        OnOff = true;
    }
}
