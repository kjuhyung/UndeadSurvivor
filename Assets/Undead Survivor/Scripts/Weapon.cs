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

    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0f; // Ÿ�̸� ������ �߰��ؼ� ����ӵ� ���
                    Fire();
                }
                break;
        }

        // ..Test code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }

    }
    
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int i = 0; i<GameManager.instance.pool.prefabs.Length; i++)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150.0f;
                Batch();
                break;
            default:
                speed = 0.3f; // ���� �ӵ�
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        // ��������� �޼�, ���Ÿ� ����� ������
        hand.spriter.sprite = data.hand;
        // sprite �̹��� data�� �־���� �̹����� ����
        hand.gameObject.SetActive(true);

        

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
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
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1, ���Ѱ���=��������
        }
    }
    void Fire()
    {
        // ��ĳ�ʿ� ���� ����� Ÿ���� ������ �Ѿ��� �����ϴ� ����
        if (!player.scanner.nearestTarget)
            return;
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; // ���� ������ �����ϰ� ���� 1�� ��ȯ

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position; // ���� ��ġ�� �Ѿ� ����
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        // ������ ���� �߽����� ��ǥ�� ���� ȸ���ϴ� �Լ�
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

    }
}
