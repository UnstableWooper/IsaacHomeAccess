using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : BossAttack
{
    [SerializeField]private Vector2 jumpStrength;

    private BossController _controller;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Damage _damage;
    private Color _ogColor;

    private GameObject _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<newPlayerMovement>().gameObject;
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = GetComponent<Damage>();
        _controller = GetComponent<BossController>();
        _spriteRenderer = _controller.spriteRenderer;
        _damage.CantDamage(false);
    }

    public override void StartAttack()
    {
        _rigidbody.AddForce(new Vector2(
            _player.transform.position.x > transform.position.x ? jumpStrength.x * 1 * Random.Range(1.2f, 0.8f) : jumpStrength.x * -1 * Random.Range(1.2f, 0.8f)
            , jumpStrength.y), ForceMode2D.Impulse);
    }
    
    public override IEnumerator AttackWarn()
    {
        if (_controller.grounded) 
        {
            _ogColor = _controller.OgColor;
            _spriteRenderer.color = Color.yellow;
            yield return new WaitForSeconds(attackWarnLength);
            _spriteRenderer.color = _ogColor;
            StartAttack();
        }
    }
}
