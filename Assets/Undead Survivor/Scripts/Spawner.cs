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
        // Mathf.Floor , Ceil �Ҽ��� �Ʒ��� ������ Int ������ �ٲٴ� �Լ�
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
