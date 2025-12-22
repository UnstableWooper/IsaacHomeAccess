using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [SerializeField, Range(0, 25)] private float Speed;

    private PlayerGun _playerGun;

    private Vector3 _direction;
    private void Start()
    {
        _playerGun = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerGun>();
        _direction = _playerGun.offset;
    }
    void Update()
    {
        transform.position += _direction * Speed * Time.deltaTime;
    }
}
