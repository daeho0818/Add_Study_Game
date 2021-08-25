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

    void Update()
    {

    }
    Bullet BulletSetting(Bullet bullet, Vector2 position)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.localPosition = position;
        bullet.transform.SetParent(bulletParent);
        return bullet;
    }

    public Bullet GetBullet(Vector2 position = new Vector2())
    {
        Bullet bullet;


        if (bulletPool.Count > 0)
        {
            bullet = bulletPool.Dequeue();
            return BulletSetting(bullet, position);
        }

        bullet = Instantiate(BulletPrefab, bulletParent);
        return BulletSetting(bullet, position);
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
