using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossHP : MonoBehaviour
{

    [SerializeField, Range(1, 100)] public int maxHP;
    private BulletProjectile _projectile;
    private int TrueHP;
    private void Start()
    {
        TrueHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            _projectile = other.GetComponent<BulletProjectile>();
            TrueHP -= _projectile.projectileDamage;
            _projectile.DestroyBullet();
            if (TrueHP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
