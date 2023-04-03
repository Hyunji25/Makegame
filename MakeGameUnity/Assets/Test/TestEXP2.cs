using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEXP2 : MonoBehaviour
{
    private int level = 0;//����
    private float currentExp = 0f;//���� ����ġ
    private float maxExp = 10f;//�ִ� ����ġ (�����ϸ� ������)
    private GameObject expBar;//����ġ��
    private GameObject levelText;//����

    void Start()
    {
        expBar = GameObject.Find("expBar");//����ġ��
        levelText = GameObject.Find("levelText");//����

        //StartCoroutine(expIncrease());//1�ʸ��� ����ġ 1�� ����
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

        if (currentExp >= maxExp)//����ġ�� �������� ������ �����ϸ� ������
        {
            levelUp();
        }
        updateExpBar();
    }
    /*
    IEnumerator expIncrease()//1�ʸ��� ����ġ 1�� ����
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(expIncrease());//1�ʸ��� �ݺ�
    }
     */

    void levelUp()//������
    {
        currentExp = 0;
        level++;
        maxExp += 5;
        levelText.GetComponent<Text>().text = level.ToString();//���� �ؽ�Ʈ ����
    }

    void updateExpBar()//����ġ ������ ���� ����ġ�� ����
    {
        if (currentExp != 0)//�ε巯�� �������� �ִϸ��̼�
        {
            expBar.GetComponent<Image>().fillAmount = Mathf.Lerp(expBar.GetComponent<Image>().fillAmount, currentExp / maxExp, Time.deltaTime * 10);
        }
        else//�������ô� �������� �ִϸ��̼� X
        {
            expBar.GetComponent<Image>().fillAmount = 0;
        }
    }
}