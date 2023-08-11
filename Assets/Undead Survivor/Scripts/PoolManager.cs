using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
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

    public GameObject Get(int index)
    {
        GameObject select = null;
        // 선택한 비활성화된 게임오브젝트 접근
            
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            // 발견하면 select 변수에 할당
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 다 활성화 되있으면
        if (!select)
        {
            // 새롭게 생성해서 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }


        return select;
    }
}