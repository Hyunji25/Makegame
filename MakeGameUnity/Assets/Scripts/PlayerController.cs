using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �����̴� �ӵ�
    private float Speed;

    // �������� �����ϴ� ����
    private Vector3 Movement;

    // �÷��̾��� Animator ������Ҹ� �޾ƿ��� ����...
    private Animator animator;

    // �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ��� ����...
    private SpriteRenderer playerRenderer;

    // ���� üũ
    private bool onAttack; // ���� ����
    private bool onHit; // �ǰ� ����
    private bool onRolling;
    private bool onDeath;
    private bool onAlive;
    private bool onClimbing;

    // ������ �Ѿ� ����
    public GameObject BulletPrefab;

    // ������ FX ����
    public GameObject fxPrefab;

    public GameObject[] stageBack = new GameObject[7];

    // ������ �Ѿ��� �������
    private List<GameObject> Bullets = new List<GameObject>();

    // �÷��̾ ���������� �ٶ� ����
    private float Direction;

    public bool Dir;

    // �÷��̾ �ٶ󺸴� ����
    public bool DirLeft;
    public bool DirRight;

    private void Awake()
    {
        // Player�� Animator�� �޾ƿ´�
        animator = this.GetComponent<Animator>();

        // Player�� SpriteRenderer�� �޾ƿ´�
        playerRenderer = this.GetComponent<SpriteRenderer>();
    }

    // ����Ƽ �⺻ ���� �Լ�
    // �ʱⰪ�� ������ �� ���
    void Start()
    {
        // �ӵ��� �ʱ�ȭ
        Speed = 5.0f;

        // �ʱⰪ ����
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

    // ����Ƽ �⺻ ���� �Լ�
    // �����Ӹ��� �ݺ������� ����Ǵ� �Լ�
    void Update()
    {
        // �Ǽ� ���� IEEE754

        // Input.GetAxis =     -1 ~ 1 ������ ���� �Ǽ��� ��ȯ
        // Input.GetAxisRaw =     -1 or 0 or 1 ��ȯ
        float Hor = Input.GetAxisRaw("Horizontal");

        // Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�
        if (Hor != 0)
            Direction = Hor;
        else
        {
            DirLeft = false;
            DirRight = false;
        }

        // �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� ���� ����
        if (Direction < 0)
        {
            playerRenderer.flipX = DirLeft = true;

            // ���� �÷��̾ �����δ�
            transform.position += Movement;
        }

        else if (Direction > 0)
        {
            playerRenderer.flipX = false;
            DirRight = true;
        }

        // �Է� ���� ������ �÷��̾ �����δ�
        Movement = new Vector3(
        Hor * Time.deltaTime * Speed,
        0.0f,
        0.0f);

        // ���� ��Ʈ��Ű�� �Է��Ѵٸ�...
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack(); // ����

        // ���� ����ƮŰ�� �Է��Ѵٸ�...
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit(); // �ǰ�

        // �����̽��ٸ� �Է��Ѵٸ�...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �Ѿ� ������ �����Ѵ�
            GameObject Obj = Instantiate(BulletPrefab);

            // ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�
            Obj.transform.position = transform.position;

            // �Ѿ��� BulletController ��ũ��Ʈ�� �޾ƿ´�
            BulletController Controller = Obj.AddComponent<BulletController>();

            // �Ѿ� ��ũ��Ʈ ������ ���� ������ ���� �÷��̾��� ���� ������ �����Ѵ�
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            // �Ѿ� ��ũ��Ʈ ������ FX Prefab�� �����Ѵ�
            Controller.fxPrefab = fxPrefab;

            // �Ѿ��� SpriteRenderer�� �޾ƿ´�
            SpriteRenderer bulletrenderer = Obj.GetComponent<SpriteRenderer>();

            // �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�
            bulletrenderer.flipY = playerRenderer.flipX;

            // ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�
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

        // �÷��̾��� �����ӿ� ���� �̵� ����� �����Ѵ�
        animator.SetFloat("Speed", Hor);



        // deltaTime : 1�����Ӱ� 2������ ������ �ð���
    }

    private void OnAttack()
    {
        // �̹� ���� ����� ���� ���̶��
        if (onAttack)
            // �Լ��� �����Ų��
            return;

        // �Լ��� ������� �ʾҴٸ� 
        // ���� ���¸� Ȱ��ȭ�ϰ�
        onAttack = true;

        // ���� ����� �����Ų��
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // �Լ��� ����Ǹ� ���� ����� ��Ȱ��ȭ�ȴ�
        // �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�
        onAttack = false;
    }

    private void OnHit()
    {
        // �̹� �ǰ� ����� ���� ���̶��
        if (onHit)
            // �Լ��� �����Ų��
            return;

        // �Լ��� ������� �ʾҴٸ� 
        // �ǰ� ���¸� Ȱ��ȭ�ϰ�
        onHit = true;

        // �ǰ� ����� �����Ų��
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        // �Լ��� ����Ǹ� �ǰ� ����� ��Ȱ��ȭ�ȴ�
        // �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�
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