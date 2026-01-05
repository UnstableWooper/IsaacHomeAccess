using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField, Range(0, 5)] int damage;

    private newPlayerHealth _playerHealth;
    
    private bool _cantDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_cantDamage)
        {
            _playerHealth = other.GetComponent<newPlayerHealth>();
            _playerHealth.Damage(damage, transform.position);
        }
    }

    public void CantDamage(bool canDamage) {
        _cantDamage = canDamage;
    } 
}
