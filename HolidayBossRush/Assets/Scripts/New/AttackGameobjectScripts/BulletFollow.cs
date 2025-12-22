using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollow : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameObject _player;

    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(_rigidbody.transform.position, _player.transform.position, speed * Time.deltaTime);
    }
}
