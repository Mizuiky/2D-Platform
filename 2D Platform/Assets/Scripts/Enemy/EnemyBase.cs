using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private int _damage = 10;

    private Rigidbody2D _rb;

    private EnemyAnimation _enemyAnimation;

    public Rigidbody2D Rb
    {
        get => _rb;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyAnimation = GetComponentInChildren<EnemyAnimation>();

        _enemyAnimation.IdleAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            health.Damage(_damage);
        }
    }

    private void Attack()
    {
        _enemyAnimation.CallAttack();
    }

    private void OnDeath()
    {
        _enemyAnimation.KillTweenAnimation(_rb);
    }
}
