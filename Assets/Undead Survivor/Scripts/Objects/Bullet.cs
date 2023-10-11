using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        // this = �ش� Ŭ������ ������ ����
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rigid.velocity = dir *15f; // ���Ѱ����� �ƴϸ� �ӵ�����
        }               
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // �ε����� ���� �ƴϰų� ���Ѱ����̶�� ��ȯ
        if (!collision.CompareTag("Enemy") || per == -1)
            return;
        per--; // ����� �پ���

        if (per == -1)
        {
            rigid.velocity = Vector2.zero; // ���� �ӵ� �ʱ�ȭ
            gameObject.SetActive(false); // ��Ȱ��ȭ
        }
    }


}
