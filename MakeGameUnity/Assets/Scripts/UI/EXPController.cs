using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPController : MonoBehaviour
{
    private int level = 0;//����
    public float currentExp = 0f;//���� ����ġ
    private int maxExp = 5;//�ִ� ����ġ (�����ϸ� ������)
    private GameObject expBar;//����ġ��
    private GameObject levelText;//����

    void Start()
    {
        expBar = GameObject.Find("expBar");//����ġ��
        levelText = GameObject.Find("levelText");//����
        currentExp = ControllerManager.GetInstance().EXP;
    }

    void Update()
    {
        currentExp = ControllerManager.GetInstance().EXP;

        if (currentExp >= maxExp)//����ġ�� �������� ������ �����ϸ� ������
        {
            levelUp();
        }
        updateExpBar();
    }

    void levelUp()//������
    {
        ControllerManager.GetInstance().EXP -= maxExp;
        level++;
        maxExp += 5;
        levelText.GetComponent<Text>().text = level.ToString();//���� �ؽ�Ʈ ����

        ControllerManager.GetInstance().SkillPoint += 1;
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