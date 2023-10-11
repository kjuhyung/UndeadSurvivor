using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    public GameObject healthSlider;

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
        // ��Ȱ��ȭ �Ǿ��־ �������� Ű���� (true) ���ڰ� ����
    }

    private void OnEnable()
    {
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerID];
    }

    void Update()
    {
        if (!GameManager.instance.IsLive) return;
        //inputVec�� x,y�� ����      
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() // ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    {
        if (!GameManager.instance.IsLive) return;
        //�Է°�,�ӵ�,�ð��� ���� ������ rigid �� �̿��� ������ ����
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;    
        rigid.MovePosition(rigid.position + nextVec);
    }  
    
    void LateUpdate() // update �� ������ �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
    {
        if (!GameManager.instance.IsLive) return;
        anim.SetFloat("Speed",inputVec.magnitude);

        // flipx,y �� �����̴� ���⿡ ���� on,off ���ִ� �ڵ�
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
        //if (inputVec.y != 0)        
        //spriter.flipY = inputVec.y > 0;        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.IsLive) return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0)
        {
            for(int i = 2; i <transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            healthSlider.SetActive(false);
            GameManager.instance.GameOver();
        }
    }
}
