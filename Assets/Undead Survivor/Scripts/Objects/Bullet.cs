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
        // this = 해당 클래스의 변수로 접근
        this.damage = damage;
        this.per = per;

        if (per >= 0)
        {
            rigid.velocity = dir *15f; // 무한관통이 아니면 속도지정
        }               
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌게 적이 아니거나 무한관통이라면 반환
        if (!collision.CompareTag("Enemy") || per == -100)
            return;
        per--; // 관통력 줄어들기

        if (per < 0)
        {
            rigid.velocity = Vector2.zero; // 물리 속도 초기화
            gameObject.SetActive(false); // 비활성화
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false) ;
    }


}
