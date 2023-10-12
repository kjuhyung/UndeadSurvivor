using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("# Game Control")] // �ν������� �Ӽ����� ���н����ִ� Ÿ��Ʋ
    public bool IsLive;
    public float gameTime;
    public float maxGameTime;
    [Header("# Player Info")]
    public int playerID;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp;
    // ����, ų��, ����ġ ����      
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp UILevelUp;
    public Result UIResult;
    public GameObject enemyCleaner;

    public Transform UIJoy;

    void Awake()
    {
        instance = this;
    }

    public void GameStart(int id)
    {
        playerID = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        UILevelUp.Select(playerID % 2);
        Resume();

        AudioManager.instance.PlayBGM(true);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Select);
    }

    public void GameOver()
    {
        StartCoroutine(nameof(GameOverRoutine));
    }

    IEnumerator GameOverRoutine()
    {
        IsLive = false;

        yield return new WaitForSeconds(0.5f);

        UIResult.gameObject.SetActive(true);
        UIResult.Lose();
        Stop();

        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(nameof(GameVictoryRoutine));
    }

    IEnumerator GameVictoryRoutine()
    {
        IsLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        UIResult.gameObject.SetActive(true);
        UIResult.Win();
        Stop();

        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(1);
        Resume();
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (!IsLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp() // ����ġ�� ��� �޼���
    {
        if (!IsLive) return;

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
        UIJoy.localScale = Vector3.zero;
    }

    public void Resume()
    {
        IsLive = true;
        Time.timeScale = 1;
        UIJoy.localScale = Vector3.one;
    }
}

