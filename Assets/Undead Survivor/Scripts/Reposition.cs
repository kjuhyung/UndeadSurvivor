using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{

    //유니티에서 제공하는 함수, 태그가 다른 오브젝트끼리의 충돌 시 구현되는 함수
    //트리거가 체크된 콜라이더에서 나갔을 때    
    void OnTriggerExit2D(Collider2D collision)
    {
        //Area 태그가 아닐경우 return, 아래 함수를 실행하지 않는다.
        if (!collision.CompareTag("Area"))
            return;

        //플레이어의 위치와 땅(나)의 위치를 변수로 지정
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        // x,y 의 차이 변수 지정 , 절대값이여야 하므로 Mathf.Abs  
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // player 의 방향 변수 지정, 대각선일때 1.4 이므로 -1 또는 1로 만들기
        Vector3 playerDir = GameManager.instance.player.inputVec; // 1 -1 1.4 
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        // swithch ~ case : 값의 상태에 따라 로직을 나눠주는 키워드
        switch (transform.tag)
        {
            // 그라운드 일때, 두 오브젝트의 거리 차이에서
            // x축과 y축의 차이를 비교해서 수평,수직 이동하는 로직
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy":

                break;
        }
    }
}

