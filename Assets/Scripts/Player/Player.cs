using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager GM;

    Vector2[][] bulletPoints = {
    new Vector2[1]{ new Vector2(0, 0) },
    new Vector2[2] {new Vector2(-0.1f, 0), new Vector2(0.1f, 0) },
    new Vector2[3] { new Vector2(-0.2f, 0), new Vector2(0, 0), new Vector2(0.2f, 0) },
    };
    void Start()
    {
        GM = GameManager.Instance;
        StartCoroutine(FireBullet());
    }

    void Update()
    {
    }

    IEnumerator FireBullet()
    {
        while (true)
        {
            Bullet bullet = GM.bulletPool.GetBullet(transform.GetChild(0), (Vector2)transform.position);
            // bullet.Init();
            bullet.bulletInfo.MoveSpeed = 3;
            bullet.transform.localScale = new Vector2(0.3f, 0.3f);
            bullet.bulletInfo.FireDir = Vector2.up;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
