using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private float Speed;
    private Animator Anim;
    public GameObject Player;
    private Animator animator;
    private bool onSkill;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        animator = this.GetComponent<Animator>();
    }

    void Start()
    {
        Speed = 10.0f;
    }
    
    void Update()
    {
        transform.position = new Vector3(
            Player.transform.position.x,
            Player.transform.position.y + 1.0f,
            Player.transform.position.z);
        OnSkill();
        Destroy(this.gameObject, 1.0f);
    }

    private void OnSkill()
    {
        if (onSkill)
            return;

        onSkill = true;

        Anim.SetTrigger("EnemyBullet");
    }

    private void SetSkill()
    {
        onSkill = false;
    }
}
