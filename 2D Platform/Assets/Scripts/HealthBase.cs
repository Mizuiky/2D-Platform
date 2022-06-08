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

    private SpriteRenderer _entitySprite;

    private int _currentLife;

    private bool _isDeath;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _currentLife = startLife;
        _isDeath = false;

        #region Initializing Entity Sprite Color
        _entitySprite = gameObject.GetComponentInChildren<SpriteRenderer>();

        if (_entitySprite != null)
        {
            _entityColor = _entitySprite.color;
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
        _isDeath = true;

        if (_destroyOnKill)
            Destroy(gameObject, _delayToDestroy);
    }

    private void ChangeColorOnDamage()
    {     
        if(_entitySprite != null)
            StartCoroutine(DamageColorTint());
    }

    private IEnumerator DamageColorTint()
    {
        _entitySprite.color = _deathColor;

        yield return new WaitForSeconds(.2f);

        _entitySprite.color = _entityColor;
    }
}
