using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEXP2 : MonoBehaviour
{
    private int level = 0;//레벨
    private float currentExp = 0f;//현재 경험치
    private float maxExp = 10f;//최대 경험치 (도달하면 레벨업)
    private GameObject expBar;//경험치바
    private GameObject levelText;//레벨

    void Start()
    {
        expBar = GameObject.Find("expBar");//경험치바
        levelText = GameObject.Find("levelText");//레벨

        //StartCoroutine(expIncrease());//1초마다 경험치 1씩 증가
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            currentExp += 5;
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            if (currentExp <= 0)
                currentExp += 0;
            else
                currentExp -= 5;
        }

        if (currentExp >= maxExp)//경험치가 레벨업할 정도에 도달하면 레벨업
        {
            levelUp();
        }
        updateExpBar();
    }
    /*
    IEnumerator expIncrease()//1초마다 경험치 1씩 증가
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(expIncrease());//1초마다 반복
    }
     */

    void levelUp()//레벨업
    {
        currentExp = 0;
        level++;
        maxExp += 5;
        levelText.GetComponent<Text>().text = level.ToString();//레벨 텍스트 변경
    }

    void updateExpBar()//경험치 비율에 따라 경험치바 조절
    {
        if (currentExp != 0)//부드러운 선형보간 애니메이션
        {
            expBar.GetComponent<Image>().fillAmount = Mathf.Lerp(expBar.GetComponent<Image>().fillAmount, currentExp / maxExp, Time.deltaTime * 10);
        }
        else//레벨업시는 선형보간 애니메이션 X
        {
            expBar.GetComponent<Image>().fillAmount = 0;
        }
    }
}