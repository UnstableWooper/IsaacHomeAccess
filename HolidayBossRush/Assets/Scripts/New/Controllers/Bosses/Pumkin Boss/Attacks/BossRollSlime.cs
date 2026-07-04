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
    [SerializeField] private GameObject StunnedIndicator;

    private BossController _pumkinController;
    private Damage _damage;
    private Vector2 _originPos;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    public int _hits;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _pumkinController = GetComponent<BossController>();
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
            transform.position = new Vector2(20, -1);
            _velocity = new Vector2(-speed * (_pumkinController.SecondPhase ? secondPhaseSpeedMulti : 1), 0);
        }
        else
        {
            transform.position = new Vector2(-20, -1);
            _velocity = new Vector2(speed * (_pumkinController.SecondPhase ? secondPhaseSpeedMulti : 1), 0);
        }
        _rigidbody.velocity = _velocity;
    }

    private void Update()
    {
        if (transform.position.x > 21 || transform.position.x < -21)
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
            _pumkinController.attackCooldownTimer += stunLength;
            _damage.CantDamage(true);


            StartCoroutine(Stunned());

            gameObject.tag = "Boss";
            //stuneded
        }
        _rigidbody.velocity = _velocity;
    }

    public override IEnumerator AttackWarn()
    {
        _pumkinController.AttackWarn(Color.red);

        _pumkinController.attackCooldownTimer += 3;
        yield return new WaitForSeconds(attackWarnLength);

        _pumkinController.AttackWarn(Color.white);

        StartAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") && _rigidbody.velocity.x != 0)
        {
            _hits++;
        }
    }



    private IEnumerator Stunned()
    {
        StunnedIndicator.SetActive(true);
        yield return new WaitForSeconds(stunLength);
        StunnedIndicator.SetActive(false);
    }
}
