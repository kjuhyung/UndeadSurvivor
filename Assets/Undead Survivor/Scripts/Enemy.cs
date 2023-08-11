using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ӵ�,��ǥ,�������� �� ���� ���� ����
    public float speed;
    public Rigidbody2D target;
    bool isLive;

    // ����
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        //�ʱ�ȭ
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // ��ġ���̸� ���ؼ� Ÿ�ٹ������� �̵�
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // ���� �ӵ��� �̵��� ������ ���� �ʵ��� �ӵ��� ����
        rigid.velocity = Vector2.zero;
    }
}
