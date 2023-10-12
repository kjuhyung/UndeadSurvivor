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

        if (per >= 0)
        {
            rigid.velocity = dir *15f; // ���Ѱ����� �ƴϸ� �ӵ�����
        }               
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // �ε����� ���� �ƴϰų� ���Ѱ����̶�� ��ȯ
        if (!collision.CompareTag("Enemy") || per == -100)
            return;
        per--; // ����� �پ���

        if (per < 0)
        {
            rigid.velocity = Vector2.zero; // ���� �ӵ� �ʱ�ȭ
            gameObject.SetActive(false); // ��Ȱ��ȭ
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false) ;
    }


}
