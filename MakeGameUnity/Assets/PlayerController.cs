using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 움직이는 속도
    private float Speed;
    private Vector3 Movement;

    private Animator animator;

    private bool onAttack;
    private bool onHit;
    private bool onRolling;
    private bool onDeath;
    private bool onAlive;
    private bool onClimbing;

    // 유니티 기본 제공 함수
    // 초기값을 설정할 때 사용
    void Start()
    {
        // 속도를 초기화
        Speed = 5.0f;

        // Player의 Animator을
        animator = this.GetComponent<Animator>();

        onAttack = false;

        onHit = false;

        onRolling = false;

        onDeath = false;

        onAlive = false;

        onClimbing = false;
    }
    
    // 유니티 기본 제공 함수
    // 프레임마다 반복적으로 실행되는 함수
    void Update()
    {
        // 실수 연산 IEEE754

        // Input.GetAxis =     -1 ~ 1 사이의 값을 실수로 반환
        // Input.GetAxisRaw =     -1 or 0 or 1 반환
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

