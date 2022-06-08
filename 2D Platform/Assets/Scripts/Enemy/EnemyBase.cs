using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private int _damage = 10;

    [Header("Animation Fields")]

    [SerializeField]
    private float yScale;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private Ease _ease;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();

        EnemyAnimation();
    }

    private void EnemyAnimation()
    {
        _rb.transform.DOScaleY(yScale, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(_ease);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            health.Damage(_damage);
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(_rb.transform);
    }
}
