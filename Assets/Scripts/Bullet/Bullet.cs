using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletInfo
{
    public Character character;
    public int BulletCount { get; set; }
    public float BulletPower { get; set; }
    public float FireSpeed { get; set; }
    public float MoveSpeed { get; set; }
    public Vector2 Size { get; set; }
    Vector2 fireDir;
    public Vector2 FireDir
    {
        get =>
            fireDir.normalized;
        set =>
            fireDir = value.normalized;
    }

    public void Init(Character character)
    {
        this.character = character;
        BulletCount = 1;
        BulletPower = 0.25f;
        FireSpeed = 1;
        MoveSpeed = 2;
        Size = new Vector2(0.4f, 0.4f);
        FireDir = new Vector2(0, 1);
    }
}

public class Bullet : MonoBehaviour
{
    public BulletInfo bulletInfo;

    private void OnEnable()
    {
    }
    void Update()
    {
        transform.Translate(bulletInfo.FireDir * bulletInfo.MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OutCollider"))
        {
            bulletInfo.character.bulletPool.ReleaseBullet(this);
        }
    }
}
