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
    Collider2D coll;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        //�ʱ�ȭ
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    void OnEnable() // Awake ������ ����Ǵ� �Լ�
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.IsLive) return;
        // anim. �ִϸ��̼� ���� �������� �� Hit
        // isLive �� false �ų� Hit ���¸� �Ʒ� �̵� ������ ���� ���ϱ�
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        if (!GameManager.instance.IsLive) return;

        if (!isLive) return;    
        
        spriter.flipX = target.position.x < rigid.position.x;
        
        
    }

    // Ȱ��ȭ �ɶ����� target �� �����ֱ�
    

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animcon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            // ..Live, Hit Action
            anim.SetTrigger("Hit"); // �ִϸ������� Ʈ���� �۵�
            AudioManager.instance.PlaySFX(AudioManager.SFX.Hit);
        }
        else
        {
            isLive = false;              // bool �� ����
            coll.enabled = false;        // �ݶ��̴� ��Ȱ��ȭ
            rigid.simulated = false;     // ������ٵ� ��Ȱ��ȭ
            spriter.sortingOrder = 1;    // Order in layer 2���� 1�� ����
            anim.SetBool("Dead",true);   // �ִϸ������� �Ķ���� ����
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            if(GameManager.instance.IsLive)
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.Dead);
            }                
        }  
    }
    // �ڷ�ƾ (Coroutine) - �����ֱ�� �񵿱�ó�� ����Ǵ� �Լ�
    // �ڷ�ƾ���� ��ȯ�� �������̽�
    // yield - �ڷ�ƾ ��ȯ Ű����
    IEnumerator KnockBack()
    {
        // yield return null; // 1�������� ����
        // yield return new WaitForSeconds(2f); // 2�� ����
        yield return wait; // ���� �ϳ��� ���� ������ ������
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        // �÷��̾��� �ݴ���� = ������ġ - �÷��̾���ġ 
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        // ForceMode2D.Impulse = �������� ��
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
