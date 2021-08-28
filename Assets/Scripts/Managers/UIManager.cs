using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;
    GameManager GM;

    public List<Button> buttons;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GM = GameManager.Instance;
        var funcs = GM.GetUpgrades(0);

        SetButtonFuncs(funcs);
    }
    public void SetButtonFuncs(List<Functions> Funcs)
    {
        foreach (Button button in buttons)
            button.onClick.RemoveAllListeners();

        foreach (Button button in buttons)
            button.onClick.AddListener(() => { Funcs[buttons.IndexOf(button)](); });
    }

    public void FadeImg(Image image, float alpha)
    {
        StartCoroutine(_FadeImg(image, alpha));
    }

    IEnumerator _FadeImg(Image image, float alpha)
    {
        Color color;

        float operValue;
        if (alpha >= image.color.a)
            operValue = 0.01f;
        else
            operValue = -0.01f;
        while(true)
        {
            color = image.color;
            if (Mathf.Abs(color.a - alpha) <= 0.1f) yield break;
            color.a += operValue;
            yield return new WaitForSeconds(0.001f);
            image.color = color;
        }
    }
}
