using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_SKILL = 3;

    private GameObject Target;

    private Animator Anim;

    // 플레이어의 SpriteRenderer 구성요소를 받아오기 위해...
    private SpriteRenderer Brenderer;

    private Vector3 Movement;
    private Vector3 EndPoint;

    private float CoolDown;
    private float Speed;
    public float HP;

    private bool SkillAttack;
    private bool Attack;
    private bool Walk;
    private bool active;

    private int choice;

    private float curTime;
    private float coolTime = 0.5f;

    public Transform pos;
    public Vector2 boxSize;

    private void Awake()
    {
        Target = GameObject.Find("Player");

        Anim = GetComponent<Animator>();

        Brenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        CoolDown = 1.5f;
        Speed = 0.5f;
        HP = 5;

        SkillAttack = false;
        Attack = false;

        active = true;

        SkillAttack = false;
        Attack = false;
        Walk = false;
    }

    void Update()
    {
        float result = Target.transform.position.x - transform.position.x;

        if (result < 0.0f)
            Brenderer.flipX = false;
        else if (result > 0.0f)
            Brenderer.flipX = true;

        if (ControllerManager.GetInstance().DirRight)
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;

        float Distance = Vector3.Distance(Target.transform.position, transform.position);

        if (active)
        {
            active = false;
            choice = onController();
            //StartCoroutine(onCooldown());
        }
        else if (Distance < 3.0f)
        {
            onAttack();
        }
        else
        {
            switch (choice)
            {
                case STATE_WALK:
                    onWalk();
                    break;

                case STATE_ATTACK:
                    onAttack();
                    break;

                case STATE_SKILL:
                    onSkill();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            HP -= ControllerManager.GetInstance().BulletDamage;

            if (HP <= 0)
            {
                ControllerManager.GetInstance().EXP += 5;
                Anim.SetTrigger("Die");
                GetComponent<CapsuleCollider2D>().enabled = false;
                Destroy(gameObject, 1.0f);
            }
        }
    }

    private int onController()
    {
        // 행동 패턴에 대한 내용을 추가 합니다
        

        {
            // 초기화
            if (Walk)
            {
                Movement = new Vector3(0.0f, 0.0f, 0.0f);
                Anim.SetFloat("Speed", Movement.x);
                Walk = false;
            }

            if (SkillAttack)
            {
                SkillAttack = false;
            }

            if (Attack)
            {
                Attack = false;
            }
        }

        // 로직

        // 어디로 움직일지 정하는 시점에 플레이어의 위치를 도착지점으로 셋팅
        EndPoint = Target.transform.position;

        // * [return]
        // * 1 : 이동           STATE_WALK
        // * 2 : 공격          STATE_ATTACK
        // * 3 : 스킬   STATE_SKILL
        return Random.Range(STATE_WALK, STATE_SKILL + 1);
    }


    private IEnumerator onCooldown()
    {
        float fTime = CoolDown;

        while (fTime > 0.0f)
        {
            fTime -= Time.deltaTime;
            yield return null;
        }
    }

    private void onAttack()
    {
        Attack = true;
        if (curTime <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    collider.GetComponent<PlayerController>().TakeDamage(ControllerManager.GetInstance().BossDamage);
                }
            }

            Anim.SetTrigger("Attack");
            curTime = coolTime;
        }
        else
        {
            curTime -= Time.deltaTime;
        }

        active = true;
    }

    private void onWalk()
    {
        //print("onWalk");
        Walk = true;

        // 목적지에 도착할 때까지......
        float Distance = Vector3.Distance(EndPoint, transform.position);


        //print(EndPoint);
        //print(Distance);

        if (Distance > 0.5f)
        {
            Vector3 Direction = (EndPoint - transform.position).normalized;

            Movement = new Vector3(
                Speed * Direction.x,
                Speed * Direction.y,
                0.0f);

            transform.position += Movement * Time.deltaTime;
            Anim.SetFloat("Speed", Mathf.Abs(Movement.x));
        }
        else
            active = true;
    }

    private void onSkill()
    {
        {
            //print("onSkill");
        }

        active = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}

// 여기 스크립트 전체적으로 다시 학습
// active 반대로 하라했던 거 같은데 왜 안 걷지