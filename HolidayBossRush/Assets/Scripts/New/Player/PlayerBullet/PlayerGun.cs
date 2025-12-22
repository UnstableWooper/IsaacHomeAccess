using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float cooldown;

    public Vector3 offset { get; set; }

    private GameObject _player;
    private float _attackCooldown;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        _attackCooldown -= Time.deltaTime;
        offset = _player.transform.localScale.x * new Vector3(1, 0, 0);

        if (Input.GetButtonDown("Shoot") & _attackCooldown < 0)
        {
            _attackCooldown = cooldown;
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
