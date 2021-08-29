using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    GameManager GM;
    public CharUpgrade charUpgrade => GetComponent<CharUpgrade>();
    public BulletPool bulletPool;
    public BulletInfo bulletInfo { get; set; }
    public int charIndex;

    Vector2[][] bulletPoints;
    void Start()
    {
        Init();
    }
    void Update()
    {
    }
    public void Init()
    {
        GM = GameManager.Instance;

        bulletInfo = new BulletInfo();
        bulletInfo.Init(this);

        bulletPoints = new Vector2[][]{
            new Vector2[1] { new Vector2(0, 0) },
            new Vector2[2] { new Vector2(-transform.localScale.x / 2.5f, 0), new Vector2(transform.localScale.x / 2.5f, 0) },
            new Vector2[3] { new Vector2(-transform.localScale.x / 1.5f, 0), new Vector2(0, 0), new Vector2(transform.localScale.x / 1.5f, 0) },
        };

        StartCoroutine(FireBullet());
    }

    IEnumerator FireBullet()
    {
        while (true)
        {
            for (int i = 0; i < bulletInfo.BulletCount; i++)
            {
                Bullet bullet = bulletPool.GetBullet(bulletInfo, (Vector2)transform.position + bulletPoints[bulletInfo.BulletCount - 1][i]);
                bullet.transform.SetParent(transform.GetChild(0));
                bullet.bulletInfo = bulletInfo;
                bullet.transform.localScale = bulletInfo.Size;
            }
            yield return new WaitForSeconds(bulletInfo.FireSpeed);
        }
    }
}

/*
    사용한 디자인패턴
    - Singleton
    - Object Pool
    - Command
    - Bridge
    - Component
    - State

    사용한 자료구조
    - Queue
    - List
*/