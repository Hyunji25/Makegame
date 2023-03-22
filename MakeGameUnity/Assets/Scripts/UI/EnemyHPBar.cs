using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    // ����ٴ� ��ü
    public GameObject Target;

    // ������ġ ����
    private Vector3 offset;

    void Start()
    {
        // ��ġ ����
        offset = new Vector3(0.0f, 0.6f, 0.0f);
    }

    private void Update()
    {
        // WorldToScreenPoint = ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ
        // ���� �� �ִ� Ÿ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ�Ͽ�, UI�� �����Ѵ�
        transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);
    }
}


// HP�� �����ؼ� �پ��� �� ���� 