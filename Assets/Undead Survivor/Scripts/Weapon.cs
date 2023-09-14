using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //무기 ID, 프리팹 ID , 데미지, 개수, 속도 변수 선언
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
                    timer = 0f; // 타이머 변수를 추가해서 연사속도 기능
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
                speed = 0.3f; // 연사 속도
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        // 근접무기면 왼손, 원거리 무기면 오른손
        hand.spriter.sprite = data.hand;
        // sprite 이미지 data에 넣어놓은 이미지로 변경
        hand.gameObject.SetActive(true);

        

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    void Batch() // 오브젝트 배치 함수 생성
    {        
        for (int index=0; index<count; index++)
        {
            Transform bullet;
            // 기존 오브젝트를 먼저 활용
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            // 모자란 것을 풀매니저에서 가져오기
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            //rotVec 변수, 360을 count로 나눈 값으로 오브젝트 배치
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1, 무한관통=근접무기
        }
    }
    void Fire()
    {
        // 스캐너에 가장 가까운 타겟이 있으면 총알을 생성하는 로직
        if (!player.scanner.nearestTarget)
            return;
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; // 현재 방향은 유지하고 값을 1로 변환

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position; // 현재 위치에 총알 생성
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        // 지정된 축을 중심으로 목표를 향해 회전하는 함수
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

    }
}
