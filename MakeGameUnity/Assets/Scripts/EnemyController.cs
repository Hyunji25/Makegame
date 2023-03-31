using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Target;

    public float Speed;
    public int HP;
    private Animator Anim;
    private Vector3 Movement;

    private float CoolDown;
    private bool Attack;
    private bool SkillAttack;

    private float curTime;
    private float coolTime = 0.5f;

    public Transform pos;
    public Vector2 boxSize;

    private static EnemyController Instance = null;

    public static EnemyController GetInstance()
    {
        if (Instance == null)
            Instance = new EnemyController();
        return Instance;
    }

    private void Awake()
    {
        Target = GameObject.Find("Player");

        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        Speed = 0.2f;
        Movement = new Vector3(1.0f, 0.0f, 0.0f);
        HP = 3;

        Attack = false;
        SkillAttack = true;
        CoolDown = 5.0f;
    }

    void Update()
    {
        Movement = ControllerManager.GetInstance().DirRight ?
            new Vector3(Speed + 1.0f, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);

        float Distance = Vector3.Distance(Target.transform.position, transform.position);

        if (Distance < 2.5f)
        {
            if (curTime <= 0)
            {
                print("공격");
                Attack = true;

                Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.tag == "Player")
                    {
                        collider.GetComponent<PlayerController>().TakeDamage(1);
                    }
                }

                Anim.SetTrigger("Attack");
                curTime = coolTime;
            }
            else
            {
                curTime -= Time.deltaTime;
            }
        }
        else if (Distance < 10.0f && !Attack)
        {
            if (SkillAttack)
            {
                print("스킬");
                Attack = true;
                SkillAttack = false;
                StartCoroutine(Skill());
            }
        }
        else
        {
            Attack = false;
        }

        if (!Attack)
        {
            Anim.SetFloat("Speed", Movement.x);
            transform.position -= Movement * Time.deltaTime;
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
                Destroy(GameObject.Find("HP"));
            }
        }
    }

    IEnumerator Skill()
    {
        Anim.SetTrigger("Skill");

        while (CoolDown > 0.0f)
        {
            CoolDown -= Time.deltaTime;
            yield return null;
        }

        CoolDown = 10.0f;
        SkillAttack = true;
    }


    private void DestroyEnemy()
    {
        Destroy(gameObject, 0.016f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}