using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
      coll = GetComponent<Collider2D>();
    }

    //유니티에서 제공하는 함수, 태그가 다른 오브젝트끼리의 충돌 시 구현되는 함수
    //트리거가 체크된 콜라이더에서 나갔을 때    
    void OnTriggerExit2D(Collider2D collision)
    {
        //Area 태그가 아닐경우 return, 아래 함수를 실행하지 않는다.
        if (!collision.CompareTag("Area"))
            return;

        //플레이어의 위치와 나의 위치를 변수로 지정
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
             
        // swithch ~ case : 값의 상태에 따라 로직을 나눠주는 키워드
        switch (transform.tag)
        {
            // 태그가 그라운드 - 두 오브젝트의 거리 차이에서
            // x축과 y축의 차이를 비교해서 수평,수직 이동하는 로직
            case "Ground":
                // x,y 의 차이 변수 지정 , 절대값이여야 하므로 Mathf.Abs  
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                // player 의 방향 변수 지정, 대각선일때 1.4 이므로 -1 또는 1로 만들기
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

                // 태그가 에너미
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2);
                }

                break;
        }
    }
}

