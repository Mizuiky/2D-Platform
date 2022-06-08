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

    [Header("GroundCheck")]
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundLayer;

    #endregion

    #region PrivateFields

    private Rigidbody2D _rb;

    private Vector2 _velocity;
    //private Vector2 _friccion = new Vector2(-.1f, 0);

    private bool _isJumping = false;
    private bool _isRunning = false;

    private bool isGrounded;

    private float _currentSpeed;

    #endregion



    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        HandleInput();
        SetCurrentSpeed();
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        Move();
        Jump();
    }
    void OnDrawGizmosSelected()
    {   
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheck.position, .12f);
    }

    private void HandleInput()
    {
        _velocity = new Vector2(0, _rb.velocity.y);

        if (Input.GetKey(KeyCode.RightArrow))
            _velocity.x = _currentSpeed;

        else if (Input.GetKey(KeyCode.LeftArrow))
            _velocity.x = -_currentSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
            _isJumping = true;

        //if (_body.velocity.x > 0)
        //    _body.velocity += _friccion;
        //else if (_body.velocity.x < 0)
        //    _body.velocity -= _friccion;
    }

    private void SetCurrentSpeed()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);
        _currentSpeed = _isRunning ? _speedRun : _speed;
    }

    private void Move()
    {
        _rb.velocity = _velocity;
    }

    private async void Jump()
    {
        if (_isJumping)
        {    
            if (isGrounded)
            {
                _isJumping = false;
                _rb.velocity = Vector2.up * _jumpForce;
                ResetScale();

                KillAnimation();
                HandleJumpScale();     
               
                await Task.Delay(600);

                KillAnimation();
                HandleGroundScale();
            }
        }    
    }

    private void HandleJumpScale()
    {
        _rb.transform.DOScaleY(_jumpScaleY, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_ease);
        _rb.transform.DOScaleX(_jumpScaleX, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_ease);
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
        _rb.transform.localScale = Vector2.one;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, .12f, _groundLayer);
    }
}
