using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField, Range(0, 5)] int damage;
    [SerializeField] private bool OnCollionDestroy;

    private newPlayerHealth _playerHealth;

    public bool canDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            _playerHealth = other.GetComponent<newPlayerHealth>();
            _playerHealth.Damage(damage, transform.position);

            if (OnCollionDestroy == true)
            {
                DestroyThis();
            }
        }

        if (other.CompareTag("Ground") && OnCollionDestroy)
        {
            DestroyThis();
        }

    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void CantDamage(bool cantDamage)
    {
        canDamage = !cantDamage;
    }
}
