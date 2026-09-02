using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveMove : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rigidbody;

    private Vector2 _trueVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(transform.position.x <= -15 || transform.position.x >= 15)
        {
            Destroy(gameObject);
        }
    }

    public void MoveRight()
    {
        _trueVelocity = _rigidbody.velocity;
        _trueVelocity = new Vector2(1 * speed, 0);
        _rigidbody.velocity = _trueVelocity;
    }

    public void MoveLeft()
    {
        _trueVelocity = _rigidbody.velocity;
        _trueVelocity = new Vector2(-1 * speed, 0);
        _rigidbody.velocity = _trueVelocity;
    }
}
