using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    //프리펩들을 보관할 변수
    public GameObject[] prefabs;

    //풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    void Awake()
    {
        //배열의 크기 지정 = 프리팹 크기만큼
        pools = new List<GameObject>[prefabs.Length];

        //배열 안에 들어있는 각각의 리스트도 초기화
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }
}