using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using System;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField]
    private SO_Int _coins;

    public Action OnCoinCollect;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        _coins.value = 0;
        OnCoinCollect?.Invoke();
    }

    public void AddCoins(int amount = 1)
    {
        _coins.value += amount;
        OnCoinCollect?.Invoke();
    }
}
