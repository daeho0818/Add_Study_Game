using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    public BulletPool bulletPool;
    public Player player;
    public BulletControllerState BulletController { get; set; }
    BulletController1 BC1;
    // BulletController2 BC2;
    // BulletController3 BC3;

    public int playerCount = 1;
    public int Money { get; set; }
    private void Awake()
    {
        Instance = this;
        BC1 = new BulletController1();
        // BC2 = new BulletController2();
        // BC3 = new BulletController3();

        // BulletController = BC1;
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
