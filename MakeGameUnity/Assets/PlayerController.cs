using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 움직이는 속도
    private float Speed;

    // 움직임을 저장하는 벡터
    private Vector3 Movement;

    // 플레이어의 Animator 구성요소를 받아오기 위해...
    private Animator animator;

    // 플레이어의 SpriteRenderer 구성요소를 받아오기 위해...
    private SpriteRenderer playerRenderer;

    // 상태 체크
    private bool onAttack; // 공격 상태
    private bool onHit; // 피격 상태
    private bool onRolling;
    private bool onDeath;
    private bool onAlive;
    private bool onClimbing;

    // 복사할 총알 원본
    public GameObject BulletPrefab;

    // 복제된 총알의 저장공간
    private List<GameObject> Bullets = new List<GameObject>();

    // 플레이어가 마지막으로 바라본 방향
    private float Direction;

    private void Awake()
    {
        // Player의 Animator를 받아온다
        animator = this.GetComponent<Animator>();

        // Player의 SpriteRenderer를 받아온다
        playerRenderer = this.GetComponent<SpriteRenderer>();
    }

    // 유니티 기본 제공 함수
    // 초기값을 설정할 때 사용
    void Start()
    {
        // 속도를 초기화
        Speed = 5.0f;

        // 초기값 세팅
        onAttack = false;
        onHit = false;
        onRolling = false;
        onDeath = false;
        onAlive = false;
        onClimbing = false;
        Direction = 1.0f;
    }
    
    // 유니티 기본 제공 함수
    // 프레임마다 반복적으로 실행되는 함수
    void Update()
    {
        // 실수 연산 IEEE754

        // Input.GetAxis =     -1 ~ 1 사이의 값을 실수로 반환
        // Input.GetAxisRaw =     -1 or 0 or 1 반환
        float Hor = Input.GetAxisRaw("Horizontal");

        // Hor이 0이라면 멈춰있는 상태이므로 예외처리를 해준다
        if (Hor != 0)
            Direction = Hor;

        // 플레이어가 바라보고 있는 방향에 따라 이미지 반전 설정
        if (Direction < 0)
            playerRenderer.flipX = true;
        else if (Direction > 0)
            playerRenderer.flipX = false;

        // 입력 받은 값으로 플레이어를 움직인다
        Movement = new Vector3(
        Hor * Time.deltaTime * Speed,
        0.0f, 
        0.0f);

        // 좌측 컨트롤키를 입력한다면...
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack(); // 공격
        
        // 좌측 쉬프트키를 입력한다면...
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit(); // 피격

        // 스페이스바를 입력한다면...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 총알 원본을 복제한다
            GameObject Obj = Instantiate(BulletPrefab);

            // 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다
            Obj.transform.position = transform.position;

            // 총알의 BulletController 스크립트를 받아온다
            BulletController Controller =  Obj.AddComponent<BulletController>();

            // 총알 스크립트 내부의 방향 변수를 현재 플레이어의 방향 변수로 설정한다
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            // 총알의 SpriteRenderer를 받아온다
            SpriteRenderer bulletrenderer = Obj.GetComponent<SpriteRenderer>();

            // 총알의 이미지 반전 상태를 플레이어의 이미지 반전 상태로 설정한다
            bulletrenderer.flipY = playerRenderer.flipX;

            // 모든 설정이 종료되었다면 저장소에 보관한다
            Bullets.Add(Obj);
        }

        if (Input.GetKey(KeyCode.E))
            OnRolling();

        if (Input.GetKey(KeyCode.R))
            OnDeath();

        if (Input.GetKey(KeyCode.Q))
            OnAlive();

        if (Input.GetKey(KeyCode.V))
            OnClimbing();

        // 플레이어의 움직임에 따라 이동 모션을 실행한다
        animator.SetFloat("Speed", Hor);

        // 실제 플레이어를 움직인다
        transform.position += Movement;


        // deltaTime : 1프레임과 2프레임 사이의 시간차
        /*
        transform.position += new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed,
            0.0f);
        */
    }

    private void OnAttack()
    {
        // 이미 공격 모션이 진행 중이라면
        if (onAttack)
            // 함수를 종료시킨다
            return;

        // 함수가 종료되지 않았다면 
        // 공격 상태를 활성화하고
        onAttack = true;

        // 공격 모션을 실행시킨다
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // 함수가 실행되면 공격 모션이 비활성화된다
        // 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨
        onAttack = false;
    }

    private void OnHit()
    {
        // 이미 피격 모션이 진행 중이라면
        if (onHit)
            // 함수를 종료시킨다
            return;

        // 함수가 종료되지 않았다면 
        // 피격 상태를 활성화하고
        onHit = true;

        // 피격 모션을 실행시킨다
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        // 함수가 실행되면 피격 모션이 비활성화된다
        // 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨
        onHit = false;
    }

    private void OnRolling()
    {
        if (onRolling)
            return;

        onRolling = true;
        animator.SetTrigger("Rolling");
    }

    private void SetRolling()
    {
        onRolling = false;
    }

    private void OnDeath()
    {
        if (onDeath)
            return;

        onDeath = true;
        animator.SetTrigger("Death");
    }

    private void SetDeath()
    {
        onDeath = false;
    }

    private void OnAlive()
    {
        if (onAlive)
            return;

        onAlive = true;
        animator.SetTrigger("Idol");
    }

    private void SetAlive()
    {
        onAlive = false;
    }

    private void OnClimbing()
    {
        if (onClimbing)
            return;

        onClimbing = true;
        animator.SetTrigger("Climbing");
    }

    private void SetClimbing()
    {
        onClimbing = false;
    }
}

