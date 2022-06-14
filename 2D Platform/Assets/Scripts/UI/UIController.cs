using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private HUD _hud;

    private void Start()
    {
        init();
    }

    public void init()
    {
        ItemManager.Instance.OnCoinCollect += UpdateCoinPoints;
    }

    private void OnDisable()
    {
        ItemManager.Instance.OnCoinCollect -= UpdateCoinPoints;
    }

    public void UpdateCoinPoints()
    {
        _hud.SetCoinPoints();
    }
}
