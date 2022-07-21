using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class Player : MonoBehaviour, IDamageable
{
    #region Serializable Fields

    [SerializeField]
    private SO_PlayerSetup _playerSetup;

    [Header("GroundCheck")]
    [SerializeField]
    private Transform _groundCheck;

    [SerializeField]
    private PlayerHealth _health;

    [SerializeField]
    private Transform _jumpDust;

    #endregion

    #region PrivateFields

    private Rigidbody2D _rb;

    private PlayerAnimation _playerAnimation;

    private Vector2 _velocity;
    private Vector2 _resetVelocity = Vector2.zero;

    //private Vector2 _friccion = new Vector2(-.1f, 0);

    private bool _isJumping = false;
    private bool _isRunning = false;
    private bool _isGrounded;

    private float _currentSpeed;

    private bool _isDead;

    #endregion

    #region Properties

    public Rigidbody2D Rb
    {
        get => _rb;
    }

    public float Side
    {
        get => transform.localScale.x;
    }

    public PlayerHealth Health
    {
        get => _health;
    }

    public PlayerAnimation PlayerAnimation
    {
        set => _playerAnimation = value;
    }

    #endregion

    #region Events

    public static Action OnPlayerDamaged;

    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, .25f);
    }

    void Awake()
    {     
        Init();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();        
    }

    private void Init()
    {
        _isDead = false;
        _health.OnDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _health.OnDeath -= OnPlayerDeath;
    }

    void Update()
    {
        if (!_isDead)
        {
            HandleInput();
            SetCurrentSpeed();
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = IsGrounded();

        Move();
        Jump();
    }

    private void HandleInput()
    {
        _velocity = new Vector2(0, _rb.velocity.y);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _velocity.x = _currentSpeed;
            _rb.transform.localScale = new Vector3(1, 1, 1);

            _playerAnimation.CallRun(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _velocity.x = -_currentSpeed;
            _rb.transform.localScale = new Vector3(-1, 1, 1);

            _playerAnimation.CallRun(true);
        }
        else
        {
            ResetScale();
            _playerAnimation.CallRun(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
        }

        //if (_body.velocity.x > 0)
        //    _body.velocity += _friccion;
        //else if (_body.velocity.x < 0)
        //    _body.velocity -= _friccion;
    }

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXType.JUMP, _jumpDust.position);
    }

    private void SetCurrentSpeed()
    {
        _isRunning = Input.GetKey(KeyCode.Z);

        if (_isRunning)
            _playerAnimation.SetAnimationSpeed(_playerAnimation.RunAnimation);
        else
            _playerAnimation.SetAnimationSpeed(1f);

        _currentSpeed = _isRunning ? _playerSetup._speedRun : _playerSetup._speed;
    }

    private void Move()
    {
        _rb.velocity = _velocity;
    }

    private void Jump()
    {
        _playerAnimation.CallJump(_isJumping, _isGrounded);

        if (_isJumping && _isGrounded)
        {
            _rb.velocity = Vector2.up * _playerSetup._jumpForce;

            ResetScale();

            _playerAnimation.KillTweenAnimation(_rb);
            _playerAnimation.CallJumpScale();

            _isJumping = false;

            PlayJumpVFX();
        }
    }

    private void ResetScale()
    {
        if (_rb.transform.localScale.x > 0)
            _rb.transform.localScale = new Vector3(1, 1, 1);
        else
            _rb.transform.localScale = new Vector3(-1, 1, 1);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, .25f, _playerSetup._groundLayer);
    }

    private void OnPlayerDeath()
    {
        _isDead = true;

        _velocity = _resetVelocity;

        _playerAnimation.KillTweenAnimation(_rb);

        ResetScale();

        _playerAnimation.CallRun(false);
        _playerAnimation.CallDeath();
    }

    public void OnDamage()
    {
        OnPlayerDamaged?.Invoke();
    }
}
