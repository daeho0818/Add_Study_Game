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
            InvokeRepeating("FireBullet", 0, 1); // Bullet ���� �� Ŭ���� ���� State �������� �����ص� �ɵ� �׸��� FireSpeed �����ؼ� ����
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
