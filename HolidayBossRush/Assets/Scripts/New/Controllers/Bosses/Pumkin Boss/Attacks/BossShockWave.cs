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

    private int _counter;

    private bool _doneAttack = false;

    public override void StartAttack()
    {
        //Debug.Log("working?");
        Instantiate(shockwave, new Vector2(0.01f, -2.8f), Quaternion.identity);
        Instantiate(shockwave, new Vector2(-0.01f, -2.8f), Quaternion.identity);
        _damage.CantDamage(true);
        StartCoroutine(DoneAttack());
    }

    private IEnumerator DoneAttack()
    {
        yield return new WaitForSeconds(2);
        _doneAttack = true;
    }

    private void Update()
    {
        Vector3 currentPos = transform.position;
        currentPos.y = Mathf.Clamp(currentPos.y, 0f, 5f);
        transform.position = currentPos;
    }

    public override IEnumerator AttackWarn()
    {
        AnimatorStateInfo stateInfo = animatior.GetCurrentAnimatorStateInfo(0);
        Debug.Log(stateInfo);


        _damage = GetComponent<Damage>();
        _pumkinController = GetComponent<BossController>();
        _pumkinController.AttackWarn(Color.red);
        yield return new WaitForSeconds(attackWarnLength);

        if (!_pumkinController.SecondPhase)
        {
            for (int i = 0; i < attackAmountSecondPhase; i++)
            {
                StartCoroutine(Slam());
                yield return new WaitUntil(() => _doneAttack);
                Debug.Log("working?");
            }
        }
        else
        {
            for (int i = 0; i < attackAmount; i++)
            {
                StartCoroutine(Slam());
                yield return new WaitUntil(() => _doneAttack);
                Debug.Log("working?");
            }
        }
    }

    IEnumerator Slam()
    {
        _doneAttack = false;
        _damage.CantDamage(false);
        animatior.SetTrigger("Slam");
        yield return new WaitForSeconds(1);
        _pumkinController.AttackCooldownAddTimer(2);
        _pumkinController.AttackWarn(Color.white);
        StartAttack();
    }
}
