using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCloneAttack : BossAttack
{
    [SerializeField] private GameObject realStPatrick;
    [SerializeField] private GameObject fakeStPatrick;
    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private int damage_After_Use;
    [SerializeField] private BossHP hp;

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
        int RandomRealStPatrick = Random.Range(0, 3);
        for(int i = 0; i < 3; i++)
        {
            Instantiate(RandomRealStPatrick == i ? realStPatrick : fakeStPatrick, spawnPositions[i].position, Quaternion.identity);
        }   
        this.gameObject.SetActive(false);

        hp.TrueBossHp -= damage_After_Use;
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
