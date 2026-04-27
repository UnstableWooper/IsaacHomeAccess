using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossRollSlime : BossAttack
{
    [SerializeField] private float speed;
    [SerializeField] private float secondPhaseSpeedMulti;
    [SerializeField] private int rollHP;
    [SerializeField] private float stunLength;

    [SerializeField] private Animator animator;

    private BossController _controller;
    private Damage _damage;
    private Vector2 _originPos;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private int _hits;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<BossController>();
        _damage = GetComponent<Damage>();
        _velocity = _rigidbody.velocity;
        _damage.CantDamage(true);
    }

    public override void StartAttack()
    {
        _hits = 0;
        _originPos = new Vector2(0, 4);
        gameObject.tag = "SlimeMode";
        _damage.CantDamage(false);
        int randPos = Random.Range(0, 2);
        if (randPos == 0)
        {
            animator.SetTrigger("Roll_1");
        }
        else
        {
            animator.SetTrigger("Roll_2");
        }
        _rigidbody.velocity = _velocity;
    }

    private void Update()
    {
        if (transform.position.x > 20 || transform.position.x < -20)
        {
            transform.position = _originPos;
            _velocity = new Vector2(0 , 0);
            _damage.CantDamage(true);
            gameObject.tag = "Boss";
        }
        if (_hits >= rollHP)
        {
            _hits = 0;
            transform.position = _originPos;
            _velocity = new Vector2(0, 0);
            _controller.AttackCooldownTimer += stunLength;
            _damage.CantDamage(true);
            
            animator.Play("Pumpkin_Idle");

            gameObject.tag = "Boss";
            //stuneded
        }
        _rigidbody.velocity = _velocity;
    }

    public override IEnumerator AttackWarn()
    {
        _controller.AttackCooldownTimer += 3;
        yield return new WaitForSeconds(attackWarnLength);
        StartAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") && _rigidbody.velocity.x != 0)
        {
            _hits++;
        }
    }
}
