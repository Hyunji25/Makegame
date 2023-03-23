using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twist : MonoBehaviour
{
    private float Angle;
    private float Speed;

    private GameObject[] Bullet = new GameObject[2];

    private void Start()
    {
        Angle = 0.0f;
        Speed = 5.0f;

        Bullet[0] = new GameObject("TwistBullet");

        Bullet[0].AddComponent<MyGizmo>();

        Bullet[0].transform.position = new Vector3(
            transform.position.x,
            transform.position.y - 1.25f,
            0.0f);

        Bullet[1] = new GameObject("TwistBullet");

        Bullet[1].AddComponent<MyGizmo>();

        Bullet[1].transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 1.25f,
            0.0f);
    }
    void Update()
    {
        Angle += 0.5f;

        Bullet[0].transform.position = new Vector3(
            1.0f,
            Mathf.Sin(Angle * Mathf.Deg2Rad),
            0.0f) * Speed * Time.deltaTime;

        Bullet[1].transform.position = new Vector3(
            1.0f,
            Mathf.Sin(-Angle * Mathf.Deg2Rad),
            0.0f) * Speed * Time.deltaTime;
    }
}

// Twist랑 BulletPattern 스크립트 다시 확인해야함 