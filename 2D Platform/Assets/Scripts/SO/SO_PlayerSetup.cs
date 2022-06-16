using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerSetup : ScriptableObject
{
    [Header("Run Setup")]
    public float _speed;
    public float _speedRun;

    [Header("Jump Setup")]
    public int _jumpForce;

    [Header("GroundCheck")]
    public LayerMask _groundLayer;
}
