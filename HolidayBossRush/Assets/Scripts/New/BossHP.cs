using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{

    [SerializeField, Range(1, 1000)]public int maxBossHp;
    private PlayerProjectile _bulletProjectile;
    private BossController _brain;
    
    public int TrueBossHp { private set; get; }
    private void Start()
    {
        _brain = GetComponent<BossController>();
        TrueBossHp = maxBossHp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            _bulletProjectile = other.GetComponent<PlayerProjectile>();
            TrueBossHp -= _bulletProjectile.projectileDamage;
            _bulletProjectile.DestroyBullet();
            _brain.StartCoroutine("DamageIndacatorCaller");
            if (TrueBossHp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
