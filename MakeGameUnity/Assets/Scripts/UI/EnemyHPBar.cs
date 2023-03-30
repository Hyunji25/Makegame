using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    // ����ٴ� ��ü
    public GameObject Target;

    private Slider EHPBar;

    // ������ġ ����
    private Vector3 offset;

    private void Awake()
    {
        EHPBar = GetComponent<Slider>();
    }

    void Start()
    {
        // ��ġ ����
        offset = new Vector3(0.0f, 0.7f, 0.0f);
        if (EHPBar != null ) 
        {
            EHPBar.maxValue = GameObject.Find("Enemy").GetComponent<EnemyController>().HP;
            EHPBar.value = EHPBar.maxValue;
        }
    }

    private void Update()
    {
        // WorldToScreenPoint = ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ
        // ���� �� �ִ� Ÿ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ�Ͽ�, UI�� �����Ѵ�
            //transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject.Find("Enemy").GetComponent<EnemyController>().HP -= 1;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("Enemy").GetComponent<EnemyController>().HP += 1;
        }

        if (EHPBar != null)
        {
            EHPBar.value = GameObject.Find("Enemy").GetComponent<EnemyController>().HP;
            if (EHPBar.value <= 0)
            {
                Destroy(GameObject.Find("HP"));
            }
        }
    }
}