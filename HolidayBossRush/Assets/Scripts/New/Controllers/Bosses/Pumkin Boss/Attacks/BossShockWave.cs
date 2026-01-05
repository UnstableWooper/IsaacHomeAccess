using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShockWave : BossAttack
{

    [SerializeField] private int attackAmount;
    [SerializeField] private int attackAmountSecondPhase;
    [SerializeField, Range(1, 5)] private float shockWaveCooldown;

    [SerializeField] private GameObject shockwave;
    private SpriteRenderer _spriteRenderer;

    private BossController _pumkinController;
    private Color _ogColor;
    public override void StartAttack()
    {
        Instantiate(shockwave, new Vector2(0.01f, -2.8f), Quaternion.identity);
        Instantiate(shockwave, new Vector2(-0.01f, -2.8f), Quaternion.identity);
    }

    public override IEnumerator AttackWarn()
    {
        _pumkinController = GetComponent<BossController>();
        _ogColor = _pumkinController.OgColor;
        _spriteRenderer = _pumkinController.spriteRenderer;
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(attackWarnLength);
        _spriteRenderer.color = _ogColor;
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
