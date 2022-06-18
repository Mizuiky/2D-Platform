using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : AnimationBase
{
    [Header("Player Animation")]
    
    [SerializeField]
    private SO_PlayerAnimation _animation;

    private Player _player;

    public float RunAnimation
    {
        get => _animation._runAnimationSpeed;
    }

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }
    public override void Init()
    {
        base.Init();
    }

    public void CallJump(bool isJumping, bool isGrounded)
    {
        Animator.SetBool(_animation._isJumping, isJumping);
        Animator.SetBool(_animation._isGrounded, isGrounded);
    }

    public void CallJumpScale()
    {
        HandleJumpScale();
    }

    private void HandleJumpScale()
    {
        if (_player.Rb.transform.localScale.x > 0)
            _player.Rb.transform.DOScaleX(_animation._jumpScaleX, _animation._duration).SetLoops(2, LoopType.Yoyo).SetEase(_animation._jumpEase);
        else
            _player.Rb.transform.DOScaleX(-_animation._jumpScaleX, _animation._duration).SetLoops(2, LoopType.Yoyo).SetEase(_animation._jumpEase);

        _player.Rb.transform.DOScaleY(_animation._jumpScaleY, _animation._duration).SetLoops(2, LoopType.Yoyo).SetEase(_animation._jumpEase);
    }

    public void CallLandScale()
    {
        HandleLandScale();
    }

    private void HandleLandScale()
    {
        _player.Rb.transform.DOScaleY(_animation._landScaleY, _animation._landScaleDuration).SetLoops(2, LoopType.Yoyo);
    }
}
