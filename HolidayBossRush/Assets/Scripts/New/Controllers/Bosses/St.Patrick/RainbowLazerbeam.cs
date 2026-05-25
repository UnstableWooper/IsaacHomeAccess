using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowLazerbeam : BossAttack
{
    [SerializeField, Range(1, 5)] private int lazers;
    [SerializeField] private float attackTimeDif;
    public float attackLength;
    [Header("Other")]
    [SerializeField] private LazerAim lazerAimScript;

    private BossController _controller;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Damage _damage;
    private GameObject _player;

    private Color _ogColor;

    public float TrueAttackWarnLength { get; private set; }
    public Vector3 ShootPos { get; private set; }
    public void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = GetComponent<Damage>();
        _controller = GetComponent<BossController>();
        _spriteRenderer = _controller.spriteRenderer;
        _damage.CantDamage(true);
    }

    public override void StartAttack()
    {
        return;
    }

    public override IEnumerator AttackWarn()
    {
        TrueAttackWarnLength = attackWarnLength;
        ShootPos = _player.transform.position;
        _controller.AttackWarn(Color.red);
        lazerAimScript.Attack();
        yield return new WaitForSeconds(TrueAttackWarnLength);
        _controller.AttackWarn(Color.white);
        StartAttack();
    }
}
