using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowLazerbeam : BossAttack
{
    private BossController _controller;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Damage _damage;
    private GameObject _player;

    private Color _ogColor;

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
        
    }

    public override IEnumerator AttackWarn()
    {
        _ogColor = _controller.OgColor;
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(attackWarnLength);
        _spriteRenderer.color = _ogColor;
        StartAttack();
    }
}
