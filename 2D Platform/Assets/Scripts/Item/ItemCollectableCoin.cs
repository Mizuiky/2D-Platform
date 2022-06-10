using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    [SerializeField]
    private int _coinPoints = 1;

    protected override void Collect()
    {
        ItemManager.instance.AddCoins(_coinPoints);

        base.Collect();
    }

    protected override void OnCollect()
    {
        base.OnCollect();
    }
}
