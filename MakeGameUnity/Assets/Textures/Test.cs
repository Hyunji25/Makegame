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
    }

    public void PushButton()
    {
        ButtonImages[0].fillAmount = 0;
        Buttons[0].GetComponent<Button>().enabled = false;

        StartCoroutine(PushButton_Coroutine());
    }

    public void Testcase1()
    {
        cooldown = 0.5f;
        ControllerManager.GetInstance().BulletSpeed += 0.025f;
    }

    IEnumerator PushButton_Coroutine()
    {
        float cool = cooldown;
        while (ButtonImages[0].fillAmount != 1)
        {
            ButtonImages[0].fillAmount += Time.deltaTime * cool;
            yield return null;
        }
        Buttons[0].GetComponent<Button>().enabled = true;
    }

    public void Testcase2()
    {
        cooldown = 0.5f;
        print("테스트 메세지2 입니다.");
    }

    public void Testcase3()
    {
        cooldown = 0.5f;
        print("테스트 메세지3 입니다.");
    }

    public void Testcase4()
    {
        cooldown = 0.5f;
        print("테스트 메세지4 입니다.");
    }

    public void Testcase5()
    {
        cooldown = 0.5f;
        print("테스트 메세지5 입니다.");
    }
}
