using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Functions();
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    public Character[] characters;

    public int CharCount { get; set; } = 1;
    public int CharIndex { get; set; } = 0;
    public int Money { get; set; }

    RaycastHit2D hit;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }

    bool[] charClick = new bool[2] { false, false };
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 3, LayerMask.GetMask("Character"));

            if (hit.transform != null)
            {
                charClick[0] = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 3, LayerMask.GetMask("Character"));

            if (hit.transform != null)
                charClick[1] = true;
            else
                charClick[0] = charClick[1] = false;

            if(charClick[0] && charClick[1])
            {
                Character character = hit.transform.GetComponent<Character>();
                CharIndex = character.charIndex;

                UIManager.Instance.SetButtonFuncs(GetUpgrades(CharIndex));
            }
        }
    }

    public void CharCountUp()
    {
        if (CharCount >= 5) return;
        CharCount++;
        CharIndex = CharCount - 1;

        characters[CharIndex].gameObject.SetActive(true);
        characters[CharIndex].Init();
    }
    public List<Functions> GetUpgrades(int index)
    {
        if (index > CharCount - 1)
        {
            Debug.LogError("CharIndex를 초과하였ㅅ븐디ㅏ 히히");
            return null;
        }
        List<Functions> Funcs;

        Funcs = new List<Functions>() {
        CharCountUp,
        characters[index].charUpgrade.BulletPowerUp,
        characters[index].charUpgrade.BulletCountUp,
        characters[index].charUpgrade.BulletSpeedUp,
        };

        return Funcs;
    }
}
