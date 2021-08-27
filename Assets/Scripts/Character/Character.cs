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

    Vector2[][] bulletPoints = {
    new Vector2[1]{ new Vector2(0, 0) },
    new Vector2[2] {new Vector2(-0.2f, 0), new Vector2(0.2f, 0) },
    new Vector2[3] { new Vector2(-0.4f, 0), new Vector2(0, 0), new Vector2(0.4f, 0) },
    };
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
