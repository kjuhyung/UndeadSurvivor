using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

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

        if (timer > (level == 0 ? 0.5f : 0.2f))
        {            
            timer = 0f;
            Spawn();
        } 
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(level);
        enemy.transform.position
            = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
