using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    //��������� ������ ����
    public GameObject[] prefabs;

    //Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        //�迭�� ũ�� ���� = ������ ũ�⸸ŭ
        pools = new List<GameObject>[prefabs.Length];

        //�迭 �ȿ� ����ִ� ������ ����Ʈ�� �ʱ�ȭ
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }
}