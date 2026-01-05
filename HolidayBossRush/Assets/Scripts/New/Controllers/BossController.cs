using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private AttackData[] attacks;

    [SerializeField] private float attackCooldown; //8
    [SerializeField] public SpriteRenderer spriteRenderer;
    private BossHP _bossHealth;
    public Color OgColor { private set; get; }
    public float AttackCooldownTimer { set; get; }
    public bool SecondPhase { private set; get; }
    //Shoot Attack
    //Roll Attack
    //Shockwave Attack

    private void Awake()
    {
        gameObject.tag = "Boss";
        _bossHealth = GetComponent<BossHP>();
        StartCoroutine(Attack());
        OgColor = spriteRenderer.color;
    }
    private void Update()
    {
        AttackCooldownTimer -= Time.deltaTime;
        if (_bossHealth.TrueBossHp <= _bossHealth.maxBossHp / 2)//later
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
            spriteRenderer.color = OgColor;
            yield return new WaitForSeconds(0.175f);
        }

    }
}