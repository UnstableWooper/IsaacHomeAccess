using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : BossAttack
{
    [SerializeField]private Vector2 jumpStrength;
    [SerializeField]private Animator animator;

    private BossController _controller;
    private Rigidbody2D _rigidbody;
    private Damage _damage;
    private bool _grounded;

    private GameObject _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<newPlayerMovement>().gameObject;
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = GetComponent<Damage>();
        _controller = GetComponent<BossController>();
        _damage.CantDamage(false);
    }

    public override void StartAttack()
    {
        animator.SetTrigger("Jump");
        _rigidbody.AddForce(new Vector2(
            _player.transform.position.x > transform.position.x ? jumpStrength.x * 1 * Random.Range(1.2f, 0.8f) : jumpStrength.x * -1 * Random.Range(1.2f, 0.8f)
            , jumpStrength.y), ForceMode2D.Impulse);
    }

    private void Update()
    {
        _grounded = _controller.grounded;
        animator.SetFloat("VelocityY", _rigidbody.velocity.y);
        if(_grounded) animator.SetTrigger("Grounded");
    }   

    public override IEnumerator AttackWarn()
    {
        if (_controller.grounded) 
        {
            _controller.AttackWarn(Color.red);
            yield return new WaitForSeconds(attackWarnLength);
            _controller.AttackWarn(Color.white);
            StartAttack();
        }
    }
}
