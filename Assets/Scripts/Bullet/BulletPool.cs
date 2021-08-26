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
        bulletParent = GameManager.Instance.player.transform.GetChild(0);
    }

    Bullet BulletSetting(Bullet bullet, Transform parent, Vector2 position)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.SetParent(parent);
        bullet.transform.position = position;
        return bullet;
    }

    public Bullet GetBullet(Transform parent, Vector2 position = new Vector2())
    {
        Bullet bullet;
        if (bulletPool.Count > 0)
        {
            bullet = bulletPool.Dequeue();
            return BulletSetting(bullet, parent, position);
        }

        bullet = Instantiate(BulletPrefab, bulletParent);
        return BulletSetting(bullet, parent, position);
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
