using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Functions();
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    public Character[] characters;

    [SerializeField] UnityEngine.UI.Image fadeImg;
    public int CharCount { get; set; } = 1;
    public int CharIndex { get; set; } = 0;
    public int Money { get; set; }

    RaycastHit2D[] hit = new RaycastHit2D[2];
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
            hit[0] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 3, LayerMask.GetMask("Character"));

            if (hit[0].transform != null)
            {
                charClick[0] = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            hit[0] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 3, LayerMask.GetMask("Character"));
            hit[1] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 3, LayerMask.GetMask("UI"));

            if (hit[0].transform != null)
                charClick[1] = true;

            else if (hit[0].transform == null && hit[1].transform == null)
            {
                Time.timeScale = 1;
                CameraManager.Instance.AddCameraMove(Vector2.zero, 100);
                CameraManager.Instance.AddCameraZoom(5, 100, 0);
                CameraManager.Instance.PlayAll();

                UIManager.Instance.FadeImg(fadeImg, 0);
            }

            if (charClick[0] && charClick[1])
            {
                Time.timeScale = 0.2f;

                CameraManager.Instance.AddCameraMove(hit[0].transform.position + Vector3.up, 100);
                CameraManager.Instance.AddCameraZoom(3, 100, 9999);
                CameraManager.Instance.PlayAll();

                UIManager.Instance.FadeImg(fadeImg, 0.75f);

                Character character = hit[0].transform.GetComponent<Character>();
                CharIndex = character.charIndex;

                UIManager.Instance.SetButtonFuncs(GetUpgrades(CharIndex));
            }
            charClick[0] = charClick[1] = false;
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
