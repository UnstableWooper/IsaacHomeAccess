using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject bullet;
    
    public int projectileDamage = 1;

    private Rigidbody2D _rigidbody2D;



    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        effect.SetActive(false);
        bullet.SetActive(true);
    }

    public void ShootProjectile(Vector2 direction, float speed)
    {
        _rigidbody2D.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, 5f);
    }

    public void DestroyBullet()
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        effect.SetActive(true);
        bullet.SetActive(false);
        Destroy(gameObject, 1);
    }
}
