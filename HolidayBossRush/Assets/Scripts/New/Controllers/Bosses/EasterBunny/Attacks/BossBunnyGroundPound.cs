using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBunnyGroundPound : BossAttack
{
    [SerializeField] private GameObject fallingEgg;
    [SerializeField] private GameObject attackWarn;

    [SerializeField] private Vector2 jumpHight;

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
        _rigidbody.AddForce(jumpHight, ForceMode2D.Impulse);
        StartCoroutine("SpecialAttack");
    }

    public IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(() => _controller.grounded);
        float Xpos = -9;
        for (int i = 0; i <= 9; i++)
        {
            int randomSpawnPick = Random.Range(0, 2);
            {
                if (randomSpawnPick == 1)
                {
                    Instantiate(attackWarn, new Vector2(Xpos, 6.5f), Quaternion.identity);
                    Instantiate(fallingEgg, new Vector2(Xpos, 10), Quaternion.identity);
                }
                Xpos += 2;
            }
        }
    }

    public override IEnumerator AttackWarn()
    {
        _controller.AttackWarn(Color.red);
        yield return new WaitForSeconds(attackWarnLength);
        _controller.AttackWarn(Color.white);
        _controller.AttackCooldownTimer += 3;
        StartAttack();
    }
}
