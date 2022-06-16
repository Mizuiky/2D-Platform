using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private SO_Health _health;

    [SerializeField]
    private EntityColorTint _entityColorTint;

    private int _currentLife;

    private bool _isDeath;

    #endregion

    #region Events

    public Action OnDeath;

    #endregion

    private void Start()
    {
        
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _currentLife = _health.startLife;
        _isDeath = false;
    }

    public void Damage(int damage)
    {
        if (_isDeath)
            return;

        _currentLife -= damage;

        _entityColorTint.ChangeColor();

        if (_currentLife <= 0)
            Kill();  
    }

    private void Kill()
    {
        _isDeath = true;

        OnDeath?.Invoke();

        if (_health._destroyOnKill)
            Destroy(gameObject, _health._delayToDestroy);
    }
}
