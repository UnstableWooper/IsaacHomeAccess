using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkinController : MonoBehaviour
{
    [SerializeField] private List<BossAttack> attacks;
    [SerializeField] private float attackCooldown; //8
    [SerializeField] public SpriteRenderer spriteRenderer;
    public float attackCooldownTimer { set; get; }

    private BossHP _bossHealth;
    public bool secondPhase { private set; get; }
    //Shoot Attack
    //Roll Attack
    //Shockwave Attack

    private void Start()
    {
        gameObject.tag = "Boss";
        _bossHealth = GetComponent<BossHP>();
        StartCoroutine(Attack());
    }
    private void Update()
    {
        attackCooldownTimer -= Time.deltaTime;
        if (_bossHealth.trueBossHP <= _bossHealth.maxBossHP / 3)//later
        {
            secondPhase = true;
            Debug.Log("secondPhase");
        }
    }
    IEnumerator Attack()
    {
        int randAttack;
        randAttack = Random.Range(0, attacks.Count);
        attacks[randAttack].StartCoroutine("AttackWarn");
        yield return new WaitUntil(() => attackCooldownTimer <= 0);
        attackCooldownTimer = attackCooldown;
        StartCoroutine(Attack());
    }
    
    public void DamageIndacatorCaller()
    {
        //Show it take damage;
    }
}
 