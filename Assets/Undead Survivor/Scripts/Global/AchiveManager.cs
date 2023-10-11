using System;
using System.Collections;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achive { UnlockPotate, UnlockBean }
    Achive[] achives;

    WaitForSecondsRealtime wait;

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void Start()
    {
        UnLockCharacter();
    }

    void UnLockCharacter()
    {
        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achiveName = achives[i].ToString();
            bool IsUnlock = PlayerPrefs.GetInt(achiveName) == 1;

            lockCharacter[i].SetActive(!IsUnlock);
            unlockCharacter[i].SetActive(IsUnlock);
        }
    }

    void LateUpdate()
    {
        foreach(Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool IsAchive = false;

        switch(achive)
        {
            case Achive.UnlockPotate:
                IsAchive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                IsAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if (IsAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int i = 0; i <uiNotice.transform.childCount; i++)
            {
                bool IsActive = i == (int)achive;
                uiNotice.transform.GetChild(i).gameObject.SetActive(IsActive);

            }

            StartCoroutine(nameof(NoticeRoutine));
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);

        yield return wait;

        uiNotice.SetActive(false);
    }
}
