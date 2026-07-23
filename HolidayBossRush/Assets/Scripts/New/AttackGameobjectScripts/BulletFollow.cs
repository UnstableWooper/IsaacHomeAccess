using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollow : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private GameObject _player;

    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(_player != null)
            transform.position = Vector2.MoveTowards(_rigidbody.transform.position, _player.transform.position, speed * Time.deltaTime);
    }

    private void Update()
    {
        if(transform.position.x > _player.transform.position.x && _player != null)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
