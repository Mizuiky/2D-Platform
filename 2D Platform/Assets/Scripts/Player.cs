using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _body;

    private Vector2 _diretion;
    private Vector2 _velocity;

    [SerializeField]
    private float _speed;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        SetInput();
    }
    private void FixedUpdate()
    {
        Move();
    }


    private void SetInput()
    {      
        if (Input.GetKey(KeyCode.RightArrow))
            _diretion = Vector2.right;

        else if (Input.GetKey(KeyCode.LeftArrow))
            _diretion = -Vector2.right;

        if (Input.GetKey(KeyCode.UpArrow))
            _diretion = Vector2.up;         
    }

  
    private void Move()
    {
        if(_diretion != Vector2.zero)
        {
            _velocity = _diretion * _speed;

            _body.MovePosition(_body.position + _velocity * Time.deltaTime);
            _diretion = Vector3.zero;
        }        
    }
}
