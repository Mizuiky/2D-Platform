using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [Header("Health Fields")]

    [SerializeField]
    private int startLife = 10;
    [SerializeField]
    private int _currentLife;
    [SerializeField]
    private float _delayToDestroy = 0.3f;
    [SerializeField]
    private bool _destroyOnKill = true;


    private EntityColorTint _entityColorTint;
   
    private bool _isDeath;

    #region Events

    public static event Action OnPlayerDeath;

    #endregion

    private void Start()
    {
        if (_entityColorTint == null)
            _entityColorTint = gameObject.GetComponent<EntityColorTint>();
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _currentLife = startLife;
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

        OnPlayerDeath?.Invoke();

        if (_destroyOnKill)
            Destroy(gameObject, _delayToDestroy);
    }
}
