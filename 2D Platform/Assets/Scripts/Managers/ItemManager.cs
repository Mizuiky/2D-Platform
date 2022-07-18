using Core.Singleton;
using System;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField]
    private SO_Int _coins;

    #region Events

    public Action OnCoinCollect;

    public delegate void OnHeartCollect(int amount);
    public event OnHeartCollect onHeartCollected;

    public delegate bool OnFilledHeart();
    public event OnFilledHeart onFilledHeart;

    #endregion

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

    public bool FillHeart(int amount)
    {
        var isHealed = false;

        if (onFilledHeart != null)
        {
            isHealed = onFilledHeart.Invoke();

            if (isHealed)
                onHeartCollected?.Invoke(amount);
        }

        return isHealed;
    }
}
