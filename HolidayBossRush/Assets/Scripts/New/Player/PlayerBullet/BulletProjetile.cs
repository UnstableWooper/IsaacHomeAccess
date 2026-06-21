using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public int projectileDamage = 1;

    private Rigidbody2D _rigidbody2D;



    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        Destroy(gameObject);
    }
}
