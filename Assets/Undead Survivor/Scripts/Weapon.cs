using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //���� ID, ������ ID , ������, ����, �ӵ� ���� ����
    public int id;
    public int prefabId;
    public int count;
    public float damage;
    public float speed;

    void Start()
    {
        Init();
    }
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }

    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150.0f;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch() // ������Ʈ ��ġ �Լ� ����
    {        
        for (int index=0; index<count; index++)
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;

            //rotVec ����, 360�� count�� ���� ������ ������Ʈ ��ġ
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1, ���Ѱ���=��������
        }
    }
}
