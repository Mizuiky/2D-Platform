using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableHeart : ItemCollectableBase
{
    [SerializeField]
    private SO_Int amountToHeal;

    protected override void Collect()
    {
        if(ItemManager.Instance.FillHeart(amountToHeal.value))
            OnCollect();
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        gameObject.SetActive(false);
    }
}
