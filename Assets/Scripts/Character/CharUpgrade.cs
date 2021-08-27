using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharUpgrade : MonoBehaviour
{
    Character character;
    void Start()
    {
        character = GetComponent<Character>();
    }
    public void BulletPowerUp()
    {
        if (character.bulletInfo.BulletPower >= 5)
            character.bulletInfo.BulletPower += 0.25f;
    }
    public void BulletCountUp()
    {
        if (character.bulletInfo.BulletCount >= 5) return;
        Debug.Log("Bullet Count UPgrade");
        character.bulletInfo.BulletCount++;
    }
    public void BulletSpeedUp()
    {
        if (character.bulletInfo.MoveSpeed >= 10) return;
        character.bulletInfo.MoveSpeed += 0.5f;
        character.bulletInfo.FireSpeed -= 0.06f;
    }
}
