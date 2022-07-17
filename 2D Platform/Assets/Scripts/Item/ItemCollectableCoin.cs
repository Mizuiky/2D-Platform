using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    [SerializeField]
    private int _coinPoints = 1;

    [SerializeField]
    private ParticleSystem _particle;

    [SerializeField]
    private GameObject _coinGraph;

    [SerializeField]
    private float _timeToHide;

    protected override void Collect()
    {
        ItemManager.Instance.AddCoins(_coinPoints);
        _collider.enabled = false;

        OnCollect();
    }

    protected override void OnCollect()
    {
        _coinGraph.SetActive(false);

        Invoke("HideOnCollect", _timeToHide);

        _particle?.Play();
    }

    private void HideOnCollect()
    {
        gameObject.SetActive(false);
    }
}
