using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float cooldown;

    [SerializeField] private Animator playerAnimator;

    public Vector3 offset { get; set; }

    private GameObject _player;
    private float _attackCooldown;

    private bool attacked;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        offset = new Vector3(1, 0, 0);
    }
    void Update()
    {
        _attackCooldown -= Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Move");
        if (vertical != 0 || horizontal != 0)
        {
            offset = new Vector3(horizontal, vertical, 0);
        }
        if (Input.GetButtonDown("Shoot") & _attackCooldown < 0)
        {
            playerAnimator.SetTrigger("Attacked");
            _attackCooldown = cooldown;
            Instantiate(bullet, transform.position, transform.rotation);

        }
    }
}
