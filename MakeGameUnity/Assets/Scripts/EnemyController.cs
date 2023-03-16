using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Text ui;
    public GameObject Player;

    public float Speed;
    public int HP;
    private Animator Anim;
    private Vector3 Movement;
    private bool onSkill;
    private GameObject BulletPrefab;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
    }

    void Start()
    {
        ui = GameObject.Find("Text (Legacy)").GetComponent<Text>();
        ui.text = null;

        Speed = 0.2f;
        Movement = new Vector3(1.0f, 0.0f, 0.0f);
        HP = 3;
    }

    void Update()
    {
        BulletController Controller = Obj.AddComponent<BulletController>();

        Movement = ControllerManager.GetInstance().DirRight ? 
            new Vector3(Speed + 1.0f, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);

        transform.position -= Movement * Time.deltaTime;
        Anim.SetFloat("Speed", Movement.x);

        float x = Player.transform.position.x - transform.position.x;
        float y = Player.transform.position.y - transform.position.y;

        float distance = Mathf.Sqrt((x * x) + (y * y));

        print(distance);

        ui.text = distance.ToString();

        if (distance < 5.0f)
        {
            OnSkill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            --HP;

            if (HP <= 0)
            {
                Anim.SetTrigger("Die");
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
    }

    private void ReleaseEnemy()
    {
        Destroy(gameObject, 0.016f);
    }

    private void OnSkill()
    {
        if (onSkill)
            return;

        onSkill = true;

        Anim.SetTrigger("Skill");
    }

    private void SetSkill()
    {
        onSkill = false;
    }
}