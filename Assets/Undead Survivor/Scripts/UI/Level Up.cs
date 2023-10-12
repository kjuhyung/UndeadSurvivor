using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySFX(AudioManager.SFX.LevelUp);
        AudioManager.instance.EffectBGM(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySFX(AudioManager.SFX.Select);
        AudioManager.instance.EffectBGM(false);
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // ��� ������ ��Ȱ��ȭ
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // �� �߿��� ���� 3�� ������ Ȱ��ȭ
        int[] random = new int[3];
        while(true)
        {
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);

            if (random[0] != random[1] && random[1] != random[2] && random[0] != random[2]) 
                break;
        }

        for (int i = 0; i < random.Length; i++)
        {
            Item ranItem = items[random[i]];

            // ���� �������� ���� �Һ���������� ��ü
            if (ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }      
    }
}
