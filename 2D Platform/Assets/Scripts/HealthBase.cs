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
    private float _delayToDestroy = 0.3f;
    [SerializeField]
    private bool _destroyOnKill = true;
    [SerializeField]
    private Color _deathColor;


    private Color _entityColor;
    private SpriteRenderer[] _entitySprites;

    private int _currentLife;

    private bool _isDeath;

    #region Events

    public static event Action OnPlayerDeath;

    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _currentLife = startLife;
        _isDeath = false;

        #region Initializing Entity Sprites Color

        _entitySprites = gameObject.GetComponentsInChildren<SpriteRenderer>();

        if (_entitySprites.Length > 0)
        {
            _entityColor = _entitySprites[0].color;
        }
        #endregion
    }

    public void Damage(int damage)
    {
        if (_isDeath)
            return;

        _currentLife -= damage;

        ChangeColorOnDamage();

        if (_currentLife <= 0)
            Kill();  
    }

    private void Kill()
    {
        Debug.Log("dead");
        _isDeath = true;

        OnPlayerDeath?.Invoke();

        if (_destroyOnKill)
            Destroy(gameObject, _delayToDestroy);
    }

    private void ChangeColorOnDamage()
    {     
        if(_entitySprites != null)
            StartCoroutine(DamageColorTint());
    }

    private IEnumerator DamageColorTint()
    {
        ChangeAllSpriteColors(_deathColor);

        yield return new WaitForSeconds(.1f);

        ChangeAllSpriteColors(_entityColor);
    }

    private void ChangeAllSpriteColors(Color color)
    {
        foreach (SpriteRenderer sprite in _entitySprites)
        {
            sprite.color = color;
        }
    }
}
