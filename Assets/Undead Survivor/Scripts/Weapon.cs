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

        // ..Test code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }

    }
    
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
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
            Transform bullet;
            // ���� ������Ʈ�� ���� Ȱ��
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            // ���ڶ� ���� Ǯ�Ŵ������� ��������
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            //rotVec ����, 360�� count�� ���� ������ ������Ʈ ��ġ
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1, ���Ѱ���=��������
        }
    }
}
