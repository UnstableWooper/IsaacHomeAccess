using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossRollSlime : BossAttack
{
    // Also When I am back can I make it so if the boss touches the ground it mkae it turn orange and also makes the ground sticky if possible of course
    [SerializeField] private float speed;
    [SerializeField] private int rollHP;

    [SerializeField] private float attackWarnLength = 1;

    private SpriteRenderer _spriteRenderer;
    private PumkinController _pumkinController;
    private Damage _damage;
    private Vector2 _originPos;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private int _hits;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _pumkinController = GetComponent<PumkinController>();
        _damage = GetComponent<Damage>();
        _spriteRenderer = _pumkinController.spriteRenderer;
        _velocity = _rigidbody.velocity;
        _damage.CanDamage = false;
    }

    public override void StartAttack()
    {
        _hits = 0;
        _originPos = new Vector2(0, 4);
        gameObject.tag = "SlimeMode";
        _damage.enabled = true;
        int randPos = Random.Range(0, 2);
        if(randPos == 1)
        {
            transform.position = new Vector2(20, -1);
            _velocity = new Vector2(-speed, 0);
        }
        else if(randPos == 0)
        {
            transform.position = new Vector2(-20, -1);
            _velocity = new Vector2(speed, 0);
        }
        _rigidbody.velocity = _velocity;
    }

    private void Update()
    {
        if (transform.position.x > 20 || transform.position.x < -20)
        {
            transform.position = _originPos;
            _velocity = new Vector2(0 , 0);
            _damage.CanDamage = false;
            gameObject.tag = "Boss";
        }
        if (_hits >= rollHP)
        {
            _hits = 0;
            transform.position = _originPos;
            _velocity = new Vector2(0, 0);
            _pumkinController.AttackCooldownTimer += 3;
            _damage.CanDamage = false;
            gameObject.tag = "Boss";
            //stuneded
        }
        _rigidbody.velocity = _velocity;
    }

    public override IEnumerator AttackWarn()
    {
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(attackWarnLength);
        _pumkinController.AttackCooldownTimer += 5;
        _spriteRenderer.color = Color.Lerp(Color.yellow, Color.red, 0.1f);
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
