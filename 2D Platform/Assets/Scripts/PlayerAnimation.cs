using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
    [Header("Player Animation")]

    [SerializeField]
    private string _boolIsRunning = "isRunning";
    [SerializeField]
    private string _boolIsDead = "isDead";
    [SerializeField]
    private string _boolIsJumping = "isJumping";
    [SerializeField]
    private string _boolIsGrounded = "isGrounded";

    private Animator _animator;
    private Player _player;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponentInParent<Player>();

        HealthBase.OnPlayerDeath += CallDeadAnimation;
    }

    private void OnDisable()
    {
        HealthBase.OnPlayerDeath -= CallDeadAnimation;
    }

    public void CallDeadAnimation()
    {
        _animator.SetBool(_boolIsDead, true);
    }

    public void CallRunAnimation(bool isRunning)
    {
        _animator.SetBool(_boolIsRunning, isRunning);
    }

    public void SetAnimationSpeed(float speed)
    {
        _animator.speed = speed;
    }
    public void CallJumpAnimation(bool isJumping, bool isGrounded)
    {
        _animator.SetBool(_boolIsJumping, isJumping);
        _animator.SetBool(_boolIsGrounded, isGrounded);
    }

    public void CallHandleGroundScale()
    {
        _player.HandleGroundScale();
    }
}
