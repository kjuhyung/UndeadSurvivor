using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("# Game Control")] // �ν������� �Ӽ����� ���н����ִ� Ÿ��Ʋ
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
    // ����, ų��, ����ġ ����      
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp UILevelUp;
    public GameObject UIResult;   

    void Awake()
    {
        instance = this;
    }

    void Start()
    {        
        health = maxHealth;

        // �ӽ� ��ũ��Ʈ
        UILevelUp.Select(0);
        IsLive = true;
    }

    public void GameOver()
    {
        StartCoroutine(nameof(GameOverRoutine));
    }

    IEnumerator GameOverRoutine()
    {
        IsLive = false;

        yield return new WaitForSeconds(0.5f);

        UIResult.SetActive(true);
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(1);
        Resume();
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

    public void GetExp() // ����ġ�� ��� �޼���
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

