using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletInfo
{
    public int BulletCount { get; set; }
    public float BulletPower { get; set; }
    public float FireSpeed { get; set; }
    public float MoveSpeed { get; set; }

    Vector2 fireDir;
    public Vector2 FireDir
    {
        get =>
            fireDir.normalized;
        set =>
            fireDir = value.normalized;
    }
    public enum Type
    {
        Player,
        Enemy
    }
    public Type BulletType { get; set; }
}

public class Bullet : MonoBehaviour
{
    public BulletInfo bulletInfo;

    private void OnEnable()
    {
        bulletInfo = new BulletInfo();
    }
    void Update()
    {
        transform.Translate(bulletInfo.FireDir * bulletInfo.MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OutCollider"))
        {
            GameManager.Instance.bulletPool.ReleaseBullet(this);
        }
    }
}
