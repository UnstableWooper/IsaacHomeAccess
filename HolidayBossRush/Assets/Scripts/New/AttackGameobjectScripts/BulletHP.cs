using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHP : MonoBehaviour
{
    [SerializeField] private int hp;

    private BulletProjectile _projectile;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            _projectile = other.GetComponent<BulletProjectile>();
            hp -= _projectile.projectileDamage;
            _projectile.DestroyBullet();
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
