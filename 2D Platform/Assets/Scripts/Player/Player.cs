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

    private PlayerAnimation _currentPlayer;

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
        _currentPlayer = Instantiate(_playerSetup.player, transform);
        
        Init();
    }

    void Start()
    {
        _health.SetTint(_currentPlayer.GetComponent<EntityColorTint>());
        _health.Init();

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

            _currentPlayer.CallRun(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _velocity.x = -_currentSpeed;
            _rb.transform.localScale = new Vector3(-1, 1, 1);

            _currentPlayer.CallRun(true);
        }
        else
        {
            ResetScale();
            _currentPlayer.CallRun(false);
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
            _currentPlayer.SetAnimationSpeed(_currentPlayer.RunAnimation);
        else
            _currentPlayer.SetAnimationSpeed(1f);

        _currentSpeed = _isRunning ? _playerSetup._speedRun : _playerSetup._speed;
    }

    private void Move()
    {
        _rb.velocity = _velocity;
    }

    private void Jump()
    {
        _currentPlayer.CallJump(_isJumping, _isGrounded);

        if (_isJumping && _isGrounded)
        {
            _rb.velocity = Vector2.up * _playerSetup._jumpForce;

            ResetScale();

            _currentPlayer.KillTweenAnimation(_rb);
            _currentPlayer.CallJumpScale();

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

        _currentPlayer.KillTweenAnimation(_rb);

        ResetScale();

        _currentPlayer.CallRun(false);
        _currentPlayer.CallDeath();
    }

    public void OnDamage()
    {
        OnPlayerDamaged?.Invoke();
    }
}
