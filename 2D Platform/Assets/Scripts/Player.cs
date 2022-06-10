using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    #region Serializable Fields

    [Header("Speed Setup")]
    [SerializeField]
    private int _jumpForce;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _speedRun;

    [Header("Animation Setup")]
    [SerializeField]
    private float _jumpScaleX;
    [SerializeField]
    private float _jumpScaleY;
    [SerializeField]
    private float _groundScaleY;
    [SerializeField]
    private Ease _ease;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private float _groundScaleDuration;
    [SerializeField]
    private float _runAnimationSpeed;

    [Header("GroundCheck")]
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundLayer;

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, .25f);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();

        Init();
    }

    private void Init()
    {       
        _isDead = false;
        HealthBase.OnPlayerDeath += OnDeath;
    }

    private void OnDisable()
    {
        HealthBase.OnPlayerDeath -= OnDeath;
    }

    void Update()
    {
        if(!_isDead)
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

            _playerAnimation.CallRunAnimation(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _velocity.x = -_currentSpeed;
            _rb.transform.localScale = new Vector3(-1, 1, 1);

            _playerAnimation.CallRunAnimation(true);
        }
        else
        {
            ResetScale();
            _playerAnimation.CallRunAnimation(false);
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

    private void SetCurrentSpeed()
    {
        _isRunning = Input.GetKey(KeyCode.Z);

        if (_isRunning)
            _playerAnimation.SetAnimationSpeed(_runAnimationSpeed);
        else
            _playerAnimation.SetAnimationSpeed(1f);

        _currentSpeed = _isRunning ? _speedRun : _speed;
    }

    private void Move()
    {
        _rb.velocity = _velocity;
    }

    private void Jump()
    {
        _playerAnimation.CallJumpAnimation(_isJumping, _isGrounded);

        if (_isJumping && _isGrounded)
        {
            _isJumping = false;
            _rb.velocity = Vector2.up * _jumpForce;
            
            ResetScale();

            KillAnimation();
            HandleJumpScale();         
        }    
    }

    private void HandleJumpScale()
    {
        if (_rb.transform.localScale.x > 0)
            _rb.transform.DOScaleX(_jumpScaleX, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_ease);
        else
            _rb.transform.DOScaleX(-_jumpScaleX, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_ease);

        _rb.transform.DOScaleY(_jumpScaleY, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_ease);      
    }

    public void HandleGroundScale()
    {
        _rb.transform.DOScaleY(_groundScaleY, _groundScaleDuration).SetLoops(2, LoopType.Yoyo);
    }

    public void KillAnimation()
    {
        DOTween.Kill(_rb.transform);
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
        return Physics2D.OverlapCircle(_groundCheck.position, .25f, _groundLayer);
    }

    private void OnDeath()
    {
        _isDead = true;

        _velocity = _resetVelocity;

        KillAnimation();

        ResetScale();
        _playerAnimation.CallRunAnimation(false);
    }
}
