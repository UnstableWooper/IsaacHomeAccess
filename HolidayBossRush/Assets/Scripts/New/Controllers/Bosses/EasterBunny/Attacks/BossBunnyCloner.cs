using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBunnyCloner : BossAttack
{
    [SerializeField] private GameObject MiniBoss;

    private BossController _controller;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Damage _damage;
    private Color _ogColor;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = GetComponent<Damage>();
        _controller = GetComponent<BossController>();
        _spriteRenderer = _controller.spriteRenderer;
        _damage.CantDamage(false);
    }
    public override void StartAttack()
    {
        if (!_controller.SecondPhase)
        {
            Instantiate(MiniBoss, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(MiniBoss, transform.position, Quaternion.identity);
            Instantiate(MiniBoss, transform.position, Quaternion.identity);
        }
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
