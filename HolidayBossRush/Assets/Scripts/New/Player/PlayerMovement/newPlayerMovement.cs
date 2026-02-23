using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlayerMovement : MonoBehaviour
{
    [SerializeField] public float maxSpeed;
    [SerializeField] public float acceleration;

    private Controller _controller;
    private Rigidbody2D _rigidbody;
    private GroundCheck _groundCheck;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _disiredPos;
    private Vector2 _velocity;

    private float _direction;
    private float _friction;

    private bool _onGround;
    private bool _locked;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = gameObject.GetComponent<Controller>();
        _groundCheck = GetComponent<GroundCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(buttonName: "lock")) {
            _locked = true;
        }
        else{
            _locked = false;
        }
            
        if (!_locked){
            _direction = _controller.input.Move();
        }
        else{
            _direction = 0;
        }
        _disiredPos.x = (maxSpeed - _friction) * _direction;
        _friction = _groundCheck.Friction;
        int direction = Mathf.RoundToInt(_direction);
        if(direction != 0)transform.localScale = new Vector2( direction == 1 ? 1 : -1, 1);
    }
    private void FixedUpdate()
    {
        _velocity = _rigidbody.velocity;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _disiredPos.x, acceleration * Time.deltaTime);
        _rigidbody.velocity = _velocity;
    }
}
