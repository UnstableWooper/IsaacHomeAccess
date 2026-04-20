using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossGoldRain : BossAttack
{
    [SerializeField] Vector2 rangeOfGold;
    [SerializeField] int rangeOfAngle;

    [SerializeField] Transform[] goldPositions;

    [Header("Other")]

    [SerializeField] private GameObject gold;
    [SerializeField] private int offset;

    private BossController _controller;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Damage _damage;
    private Color _ogColor;

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
        int goldPos = 0;

        for (int i = 0; i < goldPositions.Length; i++)
        {
            if (goldPositions[i].position.x < _controller._player.transform.position.x)
            {
                goldPos = i;
            }
        }

        for (int i = 1; i <= UnityEngine.Random.Range(Mathf.RoundToInt(rangeOfGold.x), Mathf.RoundToInt(rangeOfGold.y)); i++)
        {
            Instantiate(gold,new Vector2(goldPositions[goldPos].position.x,
                goldPositions[goldPos].position.y + offset), Quaternion.Euler(Quaternion.identity.x ,
                Quaternion.identity.y, UnityEngine.Random.Range(rangeOfAngle, -rangeOfAngle)));
        }
    }

    public override IEnumerator AttackWarn()
    {
        _ogColor = _controller.OgColor;
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(attackWarnLength);
        _spriteRenderer.color = _ogColor;
        StartAttack();
    }
}
