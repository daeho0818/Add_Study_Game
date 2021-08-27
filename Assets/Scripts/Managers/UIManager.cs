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
}
