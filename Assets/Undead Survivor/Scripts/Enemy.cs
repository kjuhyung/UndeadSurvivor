using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속도,목표,생존여부 를 위한 변수 선언
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animcon;
    public Rigidbody2D target;

    bool isLive;

    // 선언
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        //초기화
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;      
        
        // 위치차이를 구해서 타겟방향으로 이동
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // 물리 속도가 이동에 영향을 주지 않도록 속도를 제거
        rigid.velocity = Vector2.zero;
    }
    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // 활성화 될때마다 target 을 정해주기
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpwanData data)
    {
        anim.runtimeAnimatorController = animcon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            // ..Live, Hit Action

        }
        else
        {
            Dead();
        }

        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}
