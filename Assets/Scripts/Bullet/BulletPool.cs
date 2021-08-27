using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    Queue<Bullet> bulletPool = new Queue<Bullet>();

    Transform bulletParent;
    Bullet BulletPrefab => Resources.Load<Bullet>("Bullet");
    void Start()
    {
        bulletParent = GameManager.Instance.characters[0].transform.GetChild(0);
    }

    Bullet BulletSetting(Bullet bullet, BulletInfo bulletInfo, Vector2 position)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = position;
        bullet.bulletInfo = bulletInfo;
        return bullet;
    }

    public Bullet GetBullet(BulletInfo bulletInfo, Vector2 position = new Vector2())
    {
        Bullet bullet;
        if (bulletPool.Count > 0)
        {
            bullet = bulletPool.Dequeue();
            return BulletSetting(bullet, bulletInfo, position);
        }

        bullet = Instantiate(BulletPrefab, bulletParent);
        return BulletSetting(bullet, bulletInfo, position);
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
