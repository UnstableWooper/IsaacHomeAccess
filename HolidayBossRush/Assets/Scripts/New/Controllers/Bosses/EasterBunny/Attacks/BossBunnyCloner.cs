using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBunnyCloner : BossAttack
{
    [SerializeField] private GameObject Egg;

    private BossController _controller;
    private Rigidbody2D _rigidbody;
    private Damage _damage;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = GetComponent<Damage>();
        _controller = GetComponent<BossController>();
        _damage.CantDamage(false);
    }
    public override void StartAttack()
    {
        if (!_controller.SecondPhase)
        {
            Instantiate(Egg, new Vector2(transform.position.x + Random.Range(-0.25f, 0.25f), transform.position.y), Quaternion.identity);
        }
        else
        {
            Instantiate(Egg, new Vector2(transform.position.x + Random.Range(-0.5f, 0), transform.position.y), Quaternion.identity);
            Instantiate(Egg, new Vector2(transform.position.x + Random.Range(0, 0.5f), transform.position.y), Quaternion.identity);
        }
    }
    public override IEnumerator AttackWarn()
    {
        yield return new WaitForSeconds(attackWarnLength);
        StartAttack();
    }

}
