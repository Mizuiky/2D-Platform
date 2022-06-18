using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerAnimation : ScriptableObject
{

    [Header("Animation Setup")]

    public float _runAnimationSpeed;
    public string _isJumping = "isJumping";
    public string _isGrounded = "isGrounded";

    [Header("Jump Tween Animation")]

    public float _jumpScaleX;
    public float _jumpScaleY;
    public float _duration;
    public Ease _jumpEase;

    [Header("Land Tween Animation")]
    public float _landScaleY;
    public float _landScaleDuration;
    public Ease _landEase;
}
