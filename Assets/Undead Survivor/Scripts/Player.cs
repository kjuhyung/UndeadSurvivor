using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    Rigidbody2D rigid; // ����
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // �ʱ�ȭ
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void Update()
    {
        //inputVec�� x,y�� ����      
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() // ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    {
        //�Է°�,�ӵ�,�ð��� ���� ������ rigid �� �̿��� ������ ����
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;    
        rigid.MovePosition(rigid.position + nextVec);
    }  
    
    void LateUpdate() // update �� ������ �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
    {
        anim.SetFloat("Speed",inputVec.magnitude);

        // flipx,y �� �����̴� ���⿡ ���� on,off ���ִ� �ڵ�
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
        //if (inputVec.y != 0)        
        //spriter.flipY = inputVec.y > 0;
        
    }
}
