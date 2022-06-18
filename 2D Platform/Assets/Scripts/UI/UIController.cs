using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private HUD _hud;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        ItemManager.Instance.OnCoinCollect += UpdateCoinPoints;
        ItemManager.Instance.onFilledHeart += UpdateLifeContainer;

        Player.OnPlayerDamaged += UpdateLifeOnDamage;
    }

    private void OnDisable()
    {
        ItemManager.Instance.OnCoinCollect -= UpdateCoinPoints;
        ItemManager.Instance.onFilledHeart -= UpdateLifeContainer;

        Player.OnPlayerDamaged -= UpdateLifeOnDamage;
    }

    private void UpdateCoinPoints()
    {
        _hud.SetCoinPoints();
    }

    private bool UpdateLifeContainer()
    {
        return _hud.FillHeartContainer() ? true : false;     
    }

    private void UpdateLifeOnDamage()
    {
        _hud.HideHeartContainer();
    }
}
