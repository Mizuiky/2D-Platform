using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using System;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField]
    private int _coins;

    public delegate void OnCollect(int amount);
    public OnCollect OnCoinCollect;

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        _coins = 0;   
    }

    public void AddCoins(int amount = 1)
    {
        _coins += amount;
        OnCoinCollect?.Invoke(_coins);
    }
}
