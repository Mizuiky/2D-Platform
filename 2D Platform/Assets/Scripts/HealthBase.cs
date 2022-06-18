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
    private EntityColorTint _entityColorTint;

    #endregion

    #region Private Fields

    private IDamageable _damageable;

    private int _currentLife;

    private bool _isDeath;

    #endregion

    #region Events

    public Action OnDeath;

    #endregion

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _currentLife = _health.startLife;

        Debug.Log("start life" + _currentLife);
        _isDeath = false;

        _damageable = GetComponent<IDamageable>();
    }

    public void Damage(int damage)
    {
        if (_isDeath)
            return;

        if (_damageable != null)
        {
            Debug.Log("current Life" + _currentLife);
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
        Debug.Log("heal");
        Debug.Log("current Life" + _currentLife);

        _currentLife += amount;

        Debug.Log("healed");
        Debug.Log("current Life" + _currentLife);
    }
}
