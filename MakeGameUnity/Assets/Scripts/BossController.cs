using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_SLIDE = 3;

    private GameObject Target;

    private Animator Anim;

    private SpriteRenderer renderer;

    private Vector3 Movement;
    private Vector3 EndPoint;

    private float CoolDown;
    private float Speed;
    private int HP;

    private bool SkillAttack;
    private bool Attack;
    private bool Walk;
    private bool active;

    private int choice;

    private void Awake()
    {
        Target = GameObject.Find("Player");

        Anim = GetComponent<Animator>();

        renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        CoolDown = 1.5f;
        Speed = 0.5f;
        HP = 30000;

        active = false;

        SkillAttack = false;
        Attack = false;
        Walk = false;
    }

    void Update()
    {
        float result = Target.transform.position.x - transform.position.x;

        if (result < 0.0f)
            renderer.flipX = true;
        else if (result > 0.0f)
            renderer.flipX = false;


        if(ControllerManager.GetInstance().DirRight)
        {
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;
        }

        if (active)
        {


            active = true;
            StartCoroutine(onCooldown());
        }
    }

    private int onController()
    {
        // 행동 패턴에 대한 내용을 추가합니다

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

        // 로직


        // 어디로 움직일지 정하는 시점에 플레이어의 위치를 도착 지점으로 셋팅

        EndPoint = Target.transform.position;
            


        // [return]
        // 0 : 공격          Attack
        // 1 : 이동           Walk
        // 2 : 슬라이딩    SkillAttack
        return Random.Range(STATE_WALK, STATE_SLIDE + 1);
    }

    private IEnumerator onCooldown()
    {
        if (active)
        {
            active = true;
        }

        float fTime = CoolDown;

        while (fTime > 0.0f)
        {
            fTime -= Time.deltaTime;
            yield return null;
        }

        active = true;
        choice = onController();

        switch (choice)
        {
            case 0:
                onAttack();
                break;

            case 1:
                onWalk();
                break;

            case 2:
                onSlide();
                break;
        }
    }

    private void onAttack()
    {
        {
            print("onAttack");
        }

        active = false;
    }

    private void onWalk()
    {
        print("onWalk");
        Walk = true;

        // 목적지에 도착할 때까지
        float Distance = Vector3.Distance(EndPoint, transform.position);

        if (Distance < 0.5f)
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
            active = false;
    }

    private void onSlide()
    {
        {
            print("onSlide");
        }
    }
}

// active 반대로 써야함
// 여기 스크립트 전체적으로 다시 학습