using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkinController : MonoBehaviour
{
    [SerializeField] private AttackData[] attacks;

    [SerializeField] private float attackCooldown; //8
    [SerializeField] public SpriteRenderer spriteRenderer;
    private BossHP _bossHealth;
    private Color _ogColor;
    public float AttackCooldownTimer { set; get; }
    public bool SecondPhase { private set; get; }
    //Shoot Attack
    //Roll Attack
    //Shockwave Attack

    private void Start()
    {
        gameObject.tag = "Boss";
        _bossHealth = GetComponent<BossHP>();
        StartCoroutine(Attack());
        _ogColor = Color.Lerp(Color.yellow, Color.red, 0.1f);
    }
    private void Update()
    {
        AttackCooldownTimer -= Time.deltaTime;
        if (_bossHealth.TrueBossHp <= _bossHealth.maxBossHp / 3)//later
        {
            SecondPhase = true;
            Debug.Log("secondPhase");
        }
    }
    IEnumerator Attack()
    {
        int totalChance = 0;
        #region
        foreach (AttackData Attack in attacks)
        {
            totalChance += Attack.randomChance;
        }
        int randChance = Random.Range(0, totalChance);
        int cumulativeChance = 0;

        foreach (AttackData Attack in attacks)
        {
            cumulativeChance += Attack.randomChance;
            if (randChance <= cumulativeChance)
            {
                Attack.attack.StartCoroutine("AttackWarn");
                break;
            }
        }

        #endregion

        yield return new WaitUntil(() => AttackCooldownTimer <= 0);
        AttackCooldownTimer = attackCooldown;
        StartCoroutine(Attack());
    }
    
    public IEnumerator DamageIndacatorCaller()
    {
        for (int i = 1; i < 2; i++)
        { 
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.175f);
            spriteRenderer.color = _ogColor;
            yield return new WaitForSeconds(0.175f);
        }

    }
}