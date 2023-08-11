using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속도,목표,생존여부 를 위한 변수 선언
    public float speed;
    public Rigidbody2D target;
    bool isLive;

    // 선언
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        //초기화
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // 위치차이를 구해서 타겟방향으로 이동
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // 물리 속도가 이동에 영향을 주지 않도록 속도를 제거
        rigid.velocity = Vector2.zero;
    }
}
