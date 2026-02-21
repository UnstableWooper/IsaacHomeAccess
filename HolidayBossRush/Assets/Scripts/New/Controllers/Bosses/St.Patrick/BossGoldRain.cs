using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGoldRain : BossAttack
{
    [SerializeField] private GameObject gold;

    [SerializeField] private float offset;

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
        _damage.CantDamage(true);
    }

    public override void StartAttack()
    {
        for(int i = Random.Range(10,20); i <= 20; i++)
        {
            Instantiate(gold,new Vector2(transform.position.x , transform.position.y + offset), Quaternion.Euler(Quaternion.identity.x , Quaternion.identity.y, Random.Range(-45,45)));
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
