using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShockWave : BossAttack
{

    [SerializeField] private int attackAmount;
    [SerializeField] private int attackAmountSecondPhase;
    [SerializeField, Range(1, 5)] private float shockWaveCooldown;

    [SerializeField] private GameObject shockwave;

    [SerializeField] private Animator animatior;

    private BossController _pumkinController;
    private Damage _damage;

    public override void StartAttack()
    {
        Instantiate(shockwave, new Vector2(0.01f, -2.8f), Quaternion.identity);
        Instantiate(shockwave, new Vector2(-0.01f, -2.8f), Quaternion.identity);
        _damage.CantDamage(true);
    }

    public override IEnumerator AttackWarn()
    {
        _damage = GetComponent<Damage>();
        _pumkinController = GetComponent<BossController>();
        _pumkinController.AttackWarn(Color.red);
        yield return new WaitForSeconds(attackWarnLength);
        _damage.CantDamage(false);
        animatior.SetTrigger("Slam");
        yield return new WaitForSeconds(1);
        _pumkinController.AttackWarn(Color.white);
        if (!_pumkinController.SecondPhase)
        {   
            int randomAttackAmounts = Random.Range(attackAmount, attackAmount + 1);
            for (int i = 0; i < randomAttackAmounts; i++)
            {
                StartAttack();
                yield return new WaitForSeconds(shockWaveCooldown);
            }
        }
        else
        {
            int randomAttackAmountsSecondPhase = Random.Range(attackAmountSecondPhase, attackAmountSecondPhase + 1);
            for (int i = 0; i < randomAttackAmountsSecondPhase; i++)
            {
                StartAttack();
                yield return new WaitForSeconds(shockWaveCooldown);
            }
        }

    }
}
