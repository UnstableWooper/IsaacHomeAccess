using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : BossAttack
{
    [SerializeField] private int attackAmount;
    [SerializeField] private int attackAmountSecondPhase;

    [SerializeField]private GameObject bullet;
    [SerializeField]private Transform []_spawnPos;

    private SpriteRenderer _spriteRenderer;

    private BossController _pumkinController;
    private Color _ogColor;
    public override void StartAttack()
    {
        int randomPick = Random.Range(0, _spawnPos.Length);
        if (!_pumkinController.SecondPhase)
        {
            int randomAttackAmounts = Random.Range(attackAmount, attackAmount + 1);
            for(int i = 0; i < randomAttackAmounts; i++)
            {
                Instantiate(bullet, _spawnPos[randomPick].position, Quaternion.identity);
            }
        }
        else
        {
            int randomAttackAmountsSecondPhase = Random.Range(attackAmountSecondPhase, attackAmountSecondPhase + 1);
            for (int i = 0; i < randomAttackAmountsSecondPhase; i++)
            {
                Instantiate(bullet, _spawnPos[randomPick].position, Quaternion.identity);
            }
        }

    }
    public override IEnumerator AttackWarn()
    {
        _pumkinController = GetComponent<BossController>();
        _ogColor = _pumkinController.OgColor;
        _spriteRenderer = _pumkinController.spriteRenderer;
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(attackWarnLength);
        _spriteRenderer.color = _ogColor;
        StartAttack();
    }
}
