using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3 (0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3 (-0.15f, -0.15f, 0);
    // 오른손(원거리무기) 의 각 위치를 플레이어 기준으로 저장
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);
    // 왼손(근접무기) 의 각 위치를 플레이어 기준으로 저장

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
        // 자기 자신을 제외하고 가져오기 = 1번째 index 부터
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;
        if(isLeft) // 근접 무기. 왼손
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        
        else // 원거리 무기. 오른손
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
        // 플레이어의 flipX, 반전여부에 따라
        // 위치 및 soringOrder (보여지는 순서) 변경
    }
}
