using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �����̴� �ӵ�
    private float Speed;
    private Vector3 Movement;

    private Animator animator;

    private bool onAttack;
    private bool onHit;
    private bool onRolling;
    private bool onDeath;
    private bool onAlive;
    private bool onClimbing;

    // ����Ƽ �⺻ ���� �Լ�
    // �ʱⰪ�� ������ �� ���
    void Start()
    {
        // �ӵ��� �ʱ�ȭ
        Speed = 5.0f;

        // Player�� Animator��
        animator = this.GetComponent<Animator>();

        onAttack = false;

        onHit = false;

        onRolling = false;

        onDeath = false;

        onAlive = false;

        onClimbing = false;
    }
    
    // ����Ƽ �⺻ ���� �Լ�
    // �����Ӹ��� �ݺ������� ����Ǵ� �Լ�
    void Update()
    {
        // �Ǽ� ���� IEEE754

        // Input.GetAxis =     -1 ~ 1 ������ ���� �Ǽ��� ��ȯ
        // Input.GetAxisRaw =     -1 or 0 or 1 ��ȯ
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxis("Vertical");

        
        Movement = new Vector3(
        Hor * Time.deltaTime * Speed,
        Ver * Time.deltaTime * Speed, 
        0.0f);

        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();

        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();

        if (Input.GetKey(KeyCode.Space))
            OnRolling();

        if (Input.GetKey(KeyCode.R))
            OnDeath();

        if (Input.GetKey(KeyCode.Q))
            OnAlive();

        if (Input.GetKey(KeyCode.V))
            OnClimbing();

        animator.SetFloat("Speed", Hor);
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
        if (onAttack)
            return;

        onAttack = true;
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        onAttack = false;
    }

    private void OnHit()
    {
        if (onHit)
            return;

        onHit = true;
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
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

