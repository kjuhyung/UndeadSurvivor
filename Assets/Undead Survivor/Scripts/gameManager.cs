using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("# Game Control")] // 인스펙터의 속성들을 구분시켜주는 타이틀
    public bool IsLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    // 레벨, 킬수, 경험치 변수      
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp UILevelUp;
   

    void Awake()
    {
        instance = this;
    }

    void Start()
    {        
        health = maxHealth;

        // 임시 스크립트
        UILevelUp.Select(0);
        IsLive = true;
    }
    void Update()
    {
        if (!IsLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp() // 경험치를 얻는 메서드
    {
        exp++;
        if (exp == nextExp[Mathf.Min(level,nextExp.Length-1)])
        {
            level++;
            exp = 0;
            UILevelUp.Show();
        }
    }

    public void Stop()
    {
        IsLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        IsLive = true;
        Time.timeScale = 1;
    }
}

