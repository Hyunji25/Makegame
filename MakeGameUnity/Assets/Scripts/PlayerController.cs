using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject BulletPrefab;

    // 복제할 FX 원본
    private GameObject fxPrefab;

    // 추후 list로 변경
    public GameObject[] stageBack = new GameObject[7];

    // 복제된 총알의 저장공간
    private List<GameObject> Bullets = new List<GameObject>();

    // 플레이어가 마지막으로 바라본 방향
    private float Direction;

    // 플레이어가 바라보는 방향
    public bool DirLeft;
    public bool DirRight;

    //private ControllerManager Manager = new ControllerManager();

    private void Awake()
    {
        // Player의 Animator를 받아온다
        animator = this.GetComponent<Animator>();

        // Player의 SpriteRenderer를 받아온다
        playerRenderer = this.GetComponent<SpriteRenderer>();

        // [Resources] 폴더에서 사용할 리소스를 들고온다
        BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        fxPrefab = Resources.Load("Prefabs/FX/Smoke") as GameObject;
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

        DirLeft = false;
        DirRight = false;

        for (int i = 0; i < 7; i++)
        {
            stageBack[i] = GameObject.Find(i.ToString());
        }
    }

    // 유니티 기본 제공 함수
    // 프레임마다 반복적으로 실행되는 함수
    void Update()
    {
        // 실수 연산 IEEE754

        // Input.GetAxis =     -1 ~ 1 사이의 값을 실수로 반환
        // Input.GetAxisRaw =     -1 or 0 or 1 반환
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        // 입력 받은 값으로 플레이어를 움직인다
        Movement = new Vector3(
        Hor * Time.deltaTime * Speed,
        Ver * Time.deltaTime * (Speed * 0.5f),
        0.0f);

        transform.position += new Vector3(0.0f, Movement.y, 0.0f);

        // Hor이 0이라면 멈춰있는 상태이므로 예외처리를 해준다
        if (Hor != 0)
            Direction = Hor;

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {  
            // 플레이어의 좌표가 0.1 보다 작을 때 플레이어만 움직인다
            if (transform.position.x < 0.1f)
                transform.position += Movement;
            else
            {
                ControllerManager.GetInstance().DirRight = true;
                ControllerManager.GetInstance().DirLeft = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = true;

            // 플레이어의 좌표가 -15.0 보다 클 때 플레이어만 움직인다
            if (transform.position.x > -15.0f)
            // 실제 플레이어를 움직인다
                transform.position += Movement;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = false;
        }

        // 플레이어가 바라보고 있는 방향에 따라 이미지 반전 설정
        if (Direction < 0)
        {
            playerRenderer.flipX = DirLeft = true;

            // 실제 플레이어를 움직인다
            transform.position += Movement;
        }

        else if (Direction > 0)
        {
            playerRenderer.flipX = false;
            DirRight = true;
        }

        // 스페이스바를 입력한다면...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnAttack(); // 공격

            // 총알 원본을 복제한다
            GameObject Obj = Instantiate(BulletPrefab);

            // 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다
            Obj.transform.position = transform.position;

            // 총알의 BulletController 스크립트를 받아온다
            BulletController Controller = Obj.AddComponent<BulletController>();

            // 총알 스크립트 내부의 방향 변수를 현재 플레이어의 방향 변수로 설정한다
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            // 총알 스크립트 내부의 FX Prefab을 설정한다
            Controller.fxPrefab = fxPrefab;

            // 총알의 SpriteRenderer를 받아온다
            SpriteRenderer bulletrenderer = Obj.GetComponent<SpriteRenderer>();

            // 총알의 이미지 반전 상태를 플레이어의 이미지 반전 상태로 설정한다
            bulletrenderer.flipY = playerRenderer.flipX;

            // 모든 설정이 종료되었다면 저장소에 보관한다
            Bullets.Add(Obj);
        }

        // 좌측 쉬프트키를 입력한다면...
        if (Input.GetKeyDown(KeyCode.LeftShift))
            OnHit(); // 피격

        if (Input.GetKeyDown(KeyCode.E))
            OnRolling();

        if (Input.GetKeyDown(KeyCode.R))
            OnDeath();

        if (Input.GetKeyDown(KeyCode.Q))
            OnAlive();

        if (Input.GetKeyDown(KeyCode.V))
            OnClimbing();

        // 플레이어의 움직임에 따라 이동 모션을 실행한다
        animator.SetFloat("Speed", Hor);

        // deltaTime : 1프레임과 2프레임 사이의 시간차
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

    private void OnCollisionEnter2D(Collider2D collision)
    {
        print("Call");
    }
}