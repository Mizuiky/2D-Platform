using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    [SerializeField]
    private int _coins;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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
