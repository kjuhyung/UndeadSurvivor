using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpwanData[] spwanData;

    int level;
    float timer;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        // Mathf.Floor , Ceil 소수점 아래를 버리고 Int 형으로 바꾸는 함수
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);        

        if (timer > spwanData[level].spawnTime)
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
        enemy.GetComponent<Enemy>().Init(spwanData[level]);
    }
}

//직렬화 - 개체를 저장 혹은 전송하기 위해 변환 , 속성 []
[System.Serializable]
public class SpwanData
{
    //스폰데이터 라는 클래스 추가
    // 추가할 속성들 - 타입,소환시간,체력,속도
    public float spawnTime;
    public int spriteType;    
    public int health;
    public float speed;
}
