using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAnimation : AnimationBase
{
    [Header("Animation Fields")]

    [SerializeField]
    private float yScale;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private Ease _ease;

    private EnemyBase _enemyBase;

    public override void Init()
    {
        base.Init();
        _enemyBase = gameObject.GetComponentInParent<EnemyBase>();
    }

    public void IdleAnimation()
    {
        _enemyBase.Rb.transform.DOScaleY(yScale, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(_ease);
    }

    public override void CallAttack()
    {
        KillTweenAnimation(_enemyBase.Rb);

        base.CallAttack();
    }
}
