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

    // ������ �Ѿ��� �������
    private List<GameObject> Bullets = new List<GameObject>();

    // �÷��̾ ���������� �ٶ� ����
    private float Direction;

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

        // �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� ���� ����
        if (Direction < 0)
            playerRenderer.flipX = true;
        else if (Direction > 0)
            playerRenderer.flipX = false;

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
            BulletController Controller =  Obj.AddComponent<BulletController>();

            // �Ѿ� ��ũ��Ʈ ������ ���� ������ ���� �÷��̾��� ���� ������ �����Ѵ�
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

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

        // ���� �÷��̾ �����δ�
        transform.position += Movement;


        // deltaTime : 1�����Ӱ� 2������ ������ �ð���
        /*
        transform.position += new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed,
            0.0f);
        */
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

