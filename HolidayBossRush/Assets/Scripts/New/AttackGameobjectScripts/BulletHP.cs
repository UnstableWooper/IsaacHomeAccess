using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHP : MonoBehaviour
{
    [SerializeField] private int hp;

    private PlayerProjectile _bulletProjectile;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            _bulletProjectile = other.GetComponent<PlayerProjectile>();
            hp -= _bulletProjectile.projectileDamage;
            _bulletProjectile.DestroyBullet();
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
