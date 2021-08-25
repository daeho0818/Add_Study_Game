using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] BulletPool bulletPool;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            InvokeRepeating("FireBullet", 0, 1); // Bullet 종류 별 클래스 만들어서 State 패턴으로 관리해도 될듯 그리고 FireSpeed 접근해서 쓰고
        }

        else if(Input.GetKeyUp(KeyCode.A))
        {
            CancelInvoke("FireBullet");
        }
    }

    void FireBullet()
    {
        Bullet bullet = bulletPool.GetBullet(Vector2.zero);
    }
}
