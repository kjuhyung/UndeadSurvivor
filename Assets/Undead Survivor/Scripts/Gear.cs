using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }      

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
                // 장갑은 공격속도
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
                // 신발은 이동속도
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        // 가지고 있는 모든 무기
        foreach (Weapon weapon in weapons) 
        {
            switch(weapon.id)
            {
                case 0: // 근접무기 = 회전속도
                    weapon.speed = 150 + (150 * rate);
                    break;
                default: // 원거리무기 = 날아가는 속도
                    weapon.speed = 0.5f * (1f - rate);
                    break;

            }
        }
        // 무기의 공격속도 증가
    }
    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
        // 플레이어의 이동속도 증가
    }
}
