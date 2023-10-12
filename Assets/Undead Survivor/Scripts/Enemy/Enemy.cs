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
    Collider2D coll;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        //초기화
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    void OnEnable() // Awake 다음에 실행되는 함수
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
        // anim. 애니메이션 현재 상태정보 가 Hit
        // isLive 가 false 거나 Hit 상태면 아래 이동 로직을 실행 안하기
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        if (!GameManager.instance.IsLive) return;

        if (!isLive) return;    
        
        spriter.flipX = target.position.x < rigid.position.x;
        
        
    }

    // 활성화 될때마다 target 을 정해주기
    

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
            anim.SetTrigger("Hit"); // 애니메이터의 트리거 작동
            AudioManager.instance.PlaySFX(AudioManager.SFX.Hit);
        }
        else
        {
            isLive = false;              // bool 값 변경
            coll.enabled = false;        // 콜라이더 비활성화
            rigid.simulated = false;     // 리지드바디 비활성화
            spriter.sortingOrder = 1;    // Order in layer 2에서 1로 변경
            anim.SetBool("Dead",true);   // 애니메이터의 파라미터 변경
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            if(GameManager.instance.IsLive)
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.Dead);
            }                
        }  
    }
    // 코루틴 (Coroutine) - 생명주기와 비동기처럼 실행되는 함수
    // 코루틴만의 반환형 인터페이스
    // yield - 코루틴 반환 키워드
    IEnumerator KnockBack()
    {
        // yield return null; // 1프레임을 쉬기
        // yield return new WaitForSeconds(2f); // 2초 쉬기
        yield return wait; // 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        // 플레이어의 반대방향 = 현재위치 - 플레이어위치 
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        // ForceMode2D.Impulse = 순간적인 힘
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
