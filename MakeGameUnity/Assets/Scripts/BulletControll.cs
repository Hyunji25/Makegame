using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    // �Ѿ��� ���ư��� �ӵ�
    private float Speed;
    public GameObject Target = null;

    public bool Option;
    public float Angle;

    // �ڵ� �ٸ� �� �ֳ� Ȯ��

    // �Ѿ��� �浹�� Ƚ��
    //private int hp;

    // ����Ʈ ȿ�� ����
    public GameObject fxPrefab;

    // �Ѿ��� ���ư��� �� ����
    public Vector3 Direction { get; set; }

    private void Start()
    {
        // �ӵ� �ʱⰪ
        //Speed = ControllerManager.GetInstance().BulletSpeed;
        Speed = Option ? 0.75f : 1.0f;

        // ������ ����ȭ
        if(Option)
            Direction = (Target.transform.position - transform.position);
        Direction.Normalize();

        float fAngle = getAngle(Vector3.down, Direction);

        transform.eulerAngles = new Vector3(
            0.0f, 0.0f, fAngle);

        // �浹 Ƚ���� 3���� �����Ѵ�
        //hp = 3;
    }

    void Update()
    {
        // �ǽð����� Ÿ���� ��ġ�� Ȯ���ϰ� ������ �����Ѵ�
        if (Option && Target)
        {
            Direction = (Target.transform.position - transform.position);

            float fAngle = getAngle(Vector3.down, Target.transform.position);

            transform.eulerAngles = new Vector3(
            0.0f, 0.0f, fAngle);
        }
        // �������� �ӵ���ŭ ��ġ�� ����
        transform.position += Direction * Speed * Time.deltaTime;
    }

    // �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹 Ƚ�� ����
       // --hp;

        // ����Ʈ ȿ�� ����
        GameObject Obj = Instantiate(fxPrefab);

        // ����Ʈ ȿ���� ��ġ�� ����
        Obj.transform.position = transform.position;



        // collision = �浹�� ���
        // �浹�� ����� �����Ѵ�
        if (collision.transform.tag == "wall")
            Destroy(this.gameObject);
        else
        {
            // ���� ȿ���� ������ ������ ����
            GameObject camera = new GameObject("Camera Test");

            // ���� ȿ�� ��Ʈ�ѷ� ����
            camera.AddComponent<CameraController>();
        }


        // �Ѿ��� �浹 Ƚ���� 0�� �Ǹ� �Ѿ� ����
        /*
        if (hp == 0)
            Destroy(this.gameObject, 0.016f);
        */
    }

    public float getAngle(Vector3 from, Vector3 to)
    {  
        return Quaternion.FromToRotation(Vector3.down, to - from).eulerAngles.z;
    }
}