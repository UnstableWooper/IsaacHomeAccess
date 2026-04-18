using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerJump : MonoBehaviour
{
    [SerializeField] public float jumpHight;
    [SerializeField] public int jumpPhases;

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
        {
            Jump();
        }
        if (_jump & !_onGround & _jumpsDone < jumpPhases)
        {
            _jumpsDone++;
            Jump();
        }
        if (_onGround)
        {
            _jumpsDone = 0;
        }

        playerAnimatior.SetFloat("VelocityY", _rigidbody.velocity.y);
        playerAnimatior.SetBool("OnGround", _onGround);
    }

    private void Jump()
    {
        _velocity = _rigidbody.velocity;
        _velocity.y = +jumpHight;
        _rigidbody.velocity = _velocity;
    }

}
