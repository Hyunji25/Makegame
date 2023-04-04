using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyManager() { }
    private static EnemyManager instance = null;

    private int count = 0;
    private float time = 5.0f;

    public static EnemyManager GetInstance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    // �����Ǵ� Enemy�� ��Ƶ� ���� ��ü
    private GameObject Parent;

    // Enemy�� ����� ���� ��ü
    private GameObject Prefab;
    private GameObject HPPrefab;

    // ** �÷��̾��� ���� �̵� �Ÿ�
    public float Distance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            Distance = 0.0f;

            // ���� ����Ǿ ��� ������ �� �ְ� ���ش�
            DontDestroyOnLoad(gameObject);

            // �����Ǵ� Enemy�� ��Ƶ� ���� ��ü
            Parent = new GameObject("EnemyList");

            // Enemy�� ����� ���� ��ü
            Prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
            //HPPrefab = Resources.Load("Prefabs/HP") as GameObject;
        }
    }

    // �������ڸ��� Start �Լ��� �ڷ�ƾ �Լ��� ����
    private IEnumerator Start()
    {
        while(true)
        {
            // Enemy ���� ��ü�� �����Ѵ�
            GameObject Obj = Instantiate(Prefab);

            // Ŭ���� ��ġ�� �ʱ�ȭ
            Obj.transform.position = new Vector3(18.0f, Random.Range(-5.6f, -2.7f), 0.0f);
            //Obj.transform.position = new Vector3(18.0f, -7.5f, 0.0f);

            // Ŭ���� �̸� �ʱ�ȭ
            Obj.transform.name = "Enemy";

            // Ŭ���� �������� ����
            Obj.transform.SetParent(Parent.transform);

            count += 1;
            print(count);
            if ((count % 10 == 0) && (count != 0))
            {
                time -= 0.2f;
                print(time);
            }

            // 1.5�� �޽�
            yield return new WaitForSeconds(time);
        }
    }

    private void Update()
    {
        if (ControllerManager.GetInstance().DirRight)
        {
            Distance += Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        }
    }
}


// hp�� �κ� �ڵ�