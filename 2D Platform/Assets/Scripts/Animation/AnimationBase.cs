using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationBase : MonoBehaviour, IAnimation
{
    [Header("Animations")]

    [SerializeField]
    private string _isRunning = "isRunning";
    [SerializeField]
    private string _isDead = "isDead";
    [SerializeField]
    private string _attack = "attack";

    private Animator _animator;

    #region Properties

    public Animator Animator
    {
        get => _animator;
    }
    public string IsRunning
    {
        get => _isRunning;
    }

    public string IsDead
    {
        get => _isRunning;
    }

    public string Attack
    {
        get => _attack;
    }

    #endregion

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public virtual void CallDeath()
    {
        _animator.SetBool(_isDead, true);
    }

    public virtual void CallRun(bool isRunning)
    {
        _animator.SetBool(_isRunning, isRunning);
    }

    public virtual void CallAttack()
    {
        _animator.SetTrigger(_attack);
    }

    public virtual void SetAnimationSpeed(float speed)
    {
        _animator.speed = speed;
    }

    public void KillTweenAnimation(Rigidbody2D rb)
    {
        DOTween.Kill(rb.transform);
    }
}
