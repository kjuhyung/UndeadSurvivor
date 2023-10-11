using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    public GameObject healthSlider;

    Rigidbody2D rigid; // 선언
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // 초기화
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
        // 비활성화 되어있어도 가져오는 키워드 (true) 인자값 전달
    }

    private void OnEnable()
    {
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerID];
    }

    void Update()
    {
        if (!GameManager.instance.IsLive) return;
        //inputVec의 x,y값 지정      
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() // 물리 연산 프레임마다 호출되는 생명주기 함수
    {
        if (!GameManager.instance.IsLive) return;
        //입력값,속도,시간을 곱한 값으로 rigid 를 이용해 움직임 구현
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;    
        rigid.MovePosition(rigid.position + nextVec);
    }  
    
    void LateUpdate() // update 가 끝나고 프레임이 종료 되기 전 실행되는 생명주기 함수
    {
        if (!GameManager.instance.IsLive) return;
        anim.SetFloat("Speed",inputVec.magnitude);

        // flipx,y 를 움직이는 방향에 따라 on,off 해주는 코드
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
