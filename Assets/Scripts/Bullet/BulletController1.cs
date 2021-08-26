using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BulletControllerState
{
    BulletInfo GetBulletInfo();
    void CharacterCountPlus(Transform[] points);
    void BulletCountPlus();
    void BulletPowerPlus();
    void FireSpeedPlus();
}
public class BulletController1
{

}
