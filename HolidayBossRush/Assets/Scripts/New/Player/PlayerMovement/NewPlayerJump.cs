using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerJump : MonoBehaviour
{
    [SerializeField] public float jumpHeight;
    [SerializeField] public int jumpPhases;
    
    [SerializeField] public float upwardGravityStrength;
    [SerializeField] public float downwardGravityStrength;

    [SerializeField] private Animator playerAnimatior; 

    private Rigidbody2D _rigidbody;
    private Controller _controller;
    private GroundCheck _groundCheck;

    private Vector2 _velocity;

    private bool _jump;
    private bool _onGround;

    private int _jumpsDone;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
        _groundCheck = GetComponent<GroundCheck>();
    }
    private void Update()
    {
        
        _onGround = _groundCheck.OnGround;
        _jump = _controller.input.Jump();
        if (_jump && _onGround)
            Jump();
        
        if (_jump & !_onGround & _jumpsDone < jumpPhases)
        {
            _jumpsDone++;
            Jump();
        }

        if (_rigidbody.velocity.y < 0)
            _rigidbody.gravityScale = downwardGravityStrength;
        else if (_rigidbody.velocity.y > 0)
            _rigidbody.gravityScale = upwardGravityStrength;
        else
            _rigidbody.gravityScale = 1;
        
        float velocityY = _rigidbody.velocity.y;
        Mathf.Clamp(velocityY, -7.5f, Mathf.Infinity);
        _rigidbody.velocity = new Vector2( _rigidbody.velocity.x, velocityY);
        
        if (_onGround)
        {
            _jumpsDone = 0;
        }

        playerAnimatior.SetFloat("VelocityY", _rigidbody.velocity.y);
        playerAnimatior.SetBool("OnGround", _onGround);
    }

    private void Jump()
    {
        //_velocity = _rigidbody.velocity;
        //_velocity.y = +jumpHeight;
        //_rigidbody.velocity = _velocity;
        
        float mass = _rigidbody.mass;
        
        _rigidbody.velocity = new Vector2( _rigidbody.velocity.x ,(jumpHeight + (0.5f * Time.fixedDeltaTime)) / mass);
    }
}
