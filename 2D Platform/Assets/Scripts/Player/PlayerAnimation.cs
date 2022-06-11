using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : AnimationBase
{
    [Header("Player Animation")]

    [SerializeField]
    private string _isJumping = "isJumping";
    [SerializeField]
    private string _isGrounded = "isGrounded";

    [Header("Jump Tween Animation")]
    [SerializeField]
    private float _jumpScaleX;
    [SerializeField]
    private float _jumpScaleY;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private Ease _jumpEase;

    [Header("Land Tween Animation")]
    [SerializeField]
    private float _landScaleY;
    [SerializeField]
    private float _landScaleDuration;
    [SerializeField]
    private Ease _landEase;

    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();  
    }
    public override void Init()
    {
        base.Init();
        HealthBase.OnPlayerDeath += CallDeath;
    }

    private void OnDisable()
    {
        HealthBase.OnPlayerDeath -= CallDeath;
    }

    public void CallJump(bool isJumping, bool isGrounded)
    {
        Animator.SetBool(_isJumping, isJumping);
        Animator.SetBool(_isGrounded, isGrounded);
    }

    public void CallJumpScale()
    {
        HandleJumpScale();
    }

    private void HandleJumpScale()
    {
        if (_player.Rb.transform.localScale.x > 0)
            _player.Rb.transform.DOScaleX(_jumpScaleX, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_jumpEase);
        else
            _player.Rb.transform.DOScaleX(-_jumpScaleX, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_jumpEase);

        _player.Rb.transform.DOScaleY(_jumpScaleY, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_jumpEase);
    }
 
    public void CallLandScale()
    {
        HandleLandScale();
    }

    private void HandleLandScale()
    {
        _player.Rb.transform.DOScaleY(_landScaleY, _landScaleDuration).SetLoops(2, LoopType.Yoyo);
    }
}
