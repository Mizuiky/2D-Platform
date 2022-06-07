using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PrivateFields

    private Rigidbody2D _body;

    private Vector2 _velocity;
    private Vector2 _friccion = new Vector2(-.1f, 0);

    private bool _isJumping = false;
    private bool isRunning = false;

    private float _currentSpeed;

    [SerializeField]
    private int _jumpForce;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _speedRun;

    #endregion

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Jump();
        Move();     
    }

    private void Move()
    {
        isRunning = Input.GetKey(KeyCode.Z);
        _currentSpeed = isRunning ? _speedRun : _speed;

        _velocity = new Vector2(0, _body.velocity.y);

        if (Input.GetKey(KeyCode.RightArrow))
            _velocity.x = _currentSpeed;

        else if (Input.GetKey(KeyCode.LeftArrow))
            _velocity.x = -_currentSpeed;

        _body.velocity = _velocity;

        //if (_body.velocity.x > 0)
        //    _body.velocity += _friccion;
        //else if (_body.velocity.x < 0)
        //    _body.velocity -= _friccion;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _body.velocity = Vector2.up * _jumpForce;
        }       
    }
}
