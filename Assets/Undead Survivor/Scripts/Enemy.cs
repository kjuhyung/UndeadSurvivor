using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ӵ�,��ǥ,�������� �� ���� ���� ����
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animcon;
    public Rigidbody2D target;

    bool isLive;

    // ����
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        //�ʱ�ȭ
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;      
        
        // ��ġ���̸� ���ؼ� Ÿ�ٹ������� �̵�
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // ���� �ӵ��� �̵��� ������ ���� �ʵ��� �ӵ��� ����
        rigid.velocity = Vector2.zero;
    }
    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // Ȱ��ȭ �ɶ����� target �� �����ֱ�
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
