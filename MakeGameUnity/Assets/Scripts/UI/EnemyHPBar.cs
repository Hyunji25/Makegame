using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    // 따라다닐 객체
    public GameObject Target;

    private Slider EHPBar;

    // 세부위치 조정
    private Vector3 offset;

    private void Awake()
    {
        EHPBar = GetComponent<Slider>();
    }

    void Start()
    {
        // 위치 셋팅
        offset = new Vector3(0.0f, 0.7f, 0.0f);
        if (EHPBar != null ) 
        {
            EHPBar.maxValue = GameObject.Find("Enemy").GetComponent<EnemyController>().HP;
            EHPBar.value = EHPBar.maxValue;
        }
    }

    private void Update()
    {
        // WorldToScreenPoint = 월드 좌표를 카메라 좌표로 변환
        // 월드 상에 있는 타겟의 좌표를 카메라 좌표로 변환하여, UI에 셋팅한다
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