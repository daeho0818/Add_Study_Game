using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgrade : MonoBehaviour
{
    GameManager GM;
    public Transform[] playerPoints;
    void Start()
    {
        GM = GameManager.Instance;
    }

    void Update()
    {

    }

    public void CharCountPlus()
    {
        GM.BulletController.CharacterCountPlus(playerPoints);  
    }
    public void BulletCountPlus()
    {
        GM.BulletController.BulletCountPlus();
    }
    public void BulletPowerPlus()
    {
        GM.BulletController.BulletPowerPlus();
    }
}
