using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField, Range(0, 5)] int damage;

    private newPlayerHealth _playerHealth;
    
    public bool CanDamage{set; get;}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && CanDamage)
        {
            _playerHealth = other.GetComponent<newPlayerHealth>();
            _playerHealth.Damage(damage, transform.position);
        }
    }
}
