using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Player,
        Enemy
    }
    [SerializeField] BulletType bulletType;

    Vector2 FireDir
    {
        get
        {
            if (FireDir != FireDir.normalized)
                FireDir = FireDir.normalized;
            return FireDir;
        }
        set
        {
            Vector2 dir = value;
            FireDir = dir;
            if (dir != dir.normalized)
                FireDir = dir.normalized;
        }
    }

    public float fireSpeed;

    void Update()
    {
        transform.Translate(FireDir * fireSpeed * Time.deltaTime);
    }
}
