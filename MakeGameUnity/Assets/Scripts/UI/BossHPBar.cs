using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public GameObject Target;
    private Vector3 offset;
    private Slider BHPBar;

    private void Awake()
    {
        BHPBar = GetComponent<Slider>();
    }

    void Start()
    {
        offset = new Vector3(0.0f, 4.2f, 0.0f);
        if (BHPBar != null)
        {
            BHPBar.maxValue = GameObject.Find("Boss").GetComponent<BossController>().HP;
            BHPBar.value = BHPBar.maxValue;
        }
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject.Find("Boss").GetComponent<BossController>().HP -= 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject.Find("Boss").GetComponent<BossController>().HP += 1;
        }

        if (BHPBar != null)
        {
            BHPBar.value = GameObject.Find("Boss").GetComponent<BossController>().HP;
            if (BHPBar.value <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
