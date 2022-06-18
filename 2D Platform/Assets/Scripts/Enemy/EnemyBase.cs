using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _damage = 10;

    [SerializeField]
    private HealthBase _health;

    [SerializeField]
    private EnemyAnimation _enemyAnimation;

    private Rigidbody2D _rb;

    public Rigidbody2D Rb
    {
        get => _rb;
    }

    private void Awake()
    {
        if(_health != null)
        {
            _health.OnDeath += OnEnemyDeath;
        }
    }


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();

        _enemyAnimation.IdleAnimation();
    }

    private void OnDisable()
    {
        _health.OnDeath -= OnEnemyDeath;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
            health.Damage(_damage);
    }

    public void OnEnemyDamage(int amount)
    {
        _health.Damage(amount);
    }

    public void OnDamage() { /* particles of enemy damage */ }

    private void Attack()
    {
        _enemyAnimation.CallAttack();
        //make enemy shot projectiles when player are in the enemy line/range of sight
    }

    private void OnEnemyDeath()
    {
        _enemyAnimation.KillTweenAnimation(_rb);

        _enemyAnimation.CallDeath();
    }  
}
