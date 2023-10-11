using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
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

    public GameObject Get(int index)
    {
        GameObject select = null;
        // ������ ��Ȱ��ȭ�� ���ӿ�����Ʈ ����
            
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            // �߰��ϸ� select ������ �Ҵ�
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // �� Ȱ��ȭ ��������
        if (!select)
        {
            // ���Ӱ� �����ؼ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }


        return select;
    }
}