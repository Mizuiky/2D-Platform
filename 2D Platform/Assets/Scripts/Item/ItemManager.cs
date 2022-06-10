using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField]
    private int _coins;

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
    }
}
