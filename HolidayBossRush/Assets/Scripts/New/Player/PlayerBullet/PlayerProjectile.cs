using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] public int projectileDamage;

    private void Update()
    {
        if (transform.position.x >= 15 || transform.position.x <= -15 || transform.position.y >= 25 || transform.position.y <= -25)
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
