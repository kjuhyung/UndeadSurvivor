using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;
    float timer;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }
    void Update()
    {
        if (!GameManager.instance.IsLive) return;
        // Mathf.Floor , Ceil 소수점 아래를 버리고 Int 형으로 바꾸는 함수
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt
            (GameManager.instance.gameTime / levelTime), spawnData.Length -1);        

        if (timer > spawnData[level].spawnTime)
        {            
            timer = 0f;
            Spawn();
        } 
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position
            = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        // 오브젝트 풀에서 가져온 오브젝트에서 Enemy 컴포넌트 접근
        // Init 호출하고 소환데이터 인자값 전달
    }
}

//직렬화 - 개체를 저장 혹은 전송하기 위해 변환 , 속성 []
[System.Serializable]
public class SpawnData
{
    //스폰데이터 라는 클래스 추가
    // 추가할 속성들 - 타입,소환시간,체력,속도
    public float spawnTime;
    public int spriteType;    
    public int health;
    public float speed;
}
