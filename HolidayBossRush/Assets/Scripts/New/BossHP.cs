using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{

    [SerializeField, Range(1, 1000)]public int maxBossHP;

    public int trueBossHP { private set; get; }
    private PlayerProjectile _bulletProjectile;
    private PumkinController _brain;

    private void Start()
    {
        _brain = GetComponent<PumkinController>();
        trueBossHP = maxBossHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            _bulletProjectile = other.GetComponent<PlayerProjectile>();
            trueBossHP -= _bulletProjectile.projectileDamage;
            _bulletProjectile.DestroyBullet();
            _brain.DamageIndacatorCaller();
            if (trueBossHP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
