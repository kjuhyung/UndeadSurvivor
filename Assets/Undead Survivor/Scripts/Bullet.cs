using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    public void Init(float damage, int per)
    {
        // this = 해당 클래스의 변수로 접근
        this.damage = damage;
        this.per = per;

    }
        
    
}
