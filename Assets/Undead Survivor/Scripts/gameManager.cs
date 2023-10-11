using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("# Game Control")] // 인스펙터의 속성들을 구분시켜주는 타이틀
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
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
    }
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp() // 경험치를 얻는 메서드
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
            UILevelUp.Show();
        }
    }
}

