using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossGoldRain : BossAttack
{
    [SerializeField] Vector2 rangeOfGold;
    [SerializeField] int rangeOfAngle;

    [SerializeField] Transform[] goldPositions;

    [SerializeField] GameObject potOfGold;

    [Header("Other")]

    [SerializeField] private GameObject gold;
    [SerializeField] private float offset;
    
    [Header("Animation")]
    
    [SerializeField] private Sprite idleAnimation;
    [SerializeField] private Sprite attackingAnimation;
    

    private BossController _controller;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Damage _damage;
    private Color _ogColor;

    private GameObject player;

    private Vector3 currentPosition;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = GetComponent<Damage>();
        _controller = GetComponent<BossController>();
        _spriteRenderer = _controller.spriteRenderer;
        _damage.CantDamage(true);
    }

    public override void StartAttack()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Transform potentialTarget in goldPositions)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }

        }
        
        for (int i = 1; i <= UnityEngine.Random.Range(Mathf.RoundToInt(rangeOfGold.x), Mathf.RoundToInt(rangeOfGold.y)); i++)
        {
            Instantiate(gold,new Vector2(Random.Range(bestTarget.position.x -offset, bestTarget.position.x +offset),
                bestTarget.position.y + offset), Quaternion.Euler(Quaternion.identity.x ,
                Quaternion.identity.y, Random.Range(rangeOfAngle, -rangeOfAngle)));
        }

        StartCoroutine(IdleAnimation());
    }

    public override IEnumerator AttackWarn()
    {
        _controller = GetComponent<BossController>();
        _spriteRenderer = _controller.spriteRenderer;
        _spriteRenderer.sprite = attackingAnimation;
        player = _controller._player.gameObject;
        currentPosition = player.transform.position;

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Transform potentialTarget in goldPositions)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }

        }

        Instantiate(potOfGold, new Vector2(bestTarget.transform.position.x, 2.5f), new Quaternion(0,0,90,90));
        //_controller.AttackWarn(Color.red);
        yield return new WaitForSeconds(attackWarnLength);
        //_controller.AttackWarn(Color.white);
        StartAttack();
    }

    public IEnumerator IdleAnimation()
    {
        yield return new WaitForSeconds(2);
        _spriteRenderer.sprite = idleAnimation;
    }
}
