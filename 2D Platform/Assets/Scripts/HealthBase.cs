using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    #region Serializable Fields

    [SerializeField]
    private SO_Health _health;

    [SerializeField]
    protected EntityColorTint _entityColorTint;

    #endregion

    #region Private Fields

    private IDamageable _damageable;

    private int _currentLife;

    private bool _isDeath;

    #endregion

    #region Events

    public Action OnDeath;

    #endregion

    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        _currentLife = _health.startLife;

        _isDeath = false;

        _damageable = GetComponent<IDamageable>();
    }

    public void Damage(int damage)
    {
        if (_isDeath)
            return;

        if (_damageable != null)
        {
            _damageable.OnDamage();     
            _currentLife -= damage;        
        }

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

    protected virtual void Heal(int amount)
    {
        _currentLife += amount;
    }
}
