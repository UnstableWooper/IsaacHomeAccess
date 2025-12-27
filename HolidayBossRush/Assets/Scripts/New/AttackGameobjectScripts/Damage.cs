using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField, Range(0, 5)] int damage;

    private newPlayerHealth _playerHealth;
    
    private bool _canDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(_canDamage + ": damage");
        if (other.CompareTag("Player") && _canDamage)
        {
            _playerHealth = other.GetComponent<newPlayerHealth>();
            _playerHealth.Damage(damage, transform.position);
        }
    }

    public void Damager(bool canDamage) {
        _canDamage = canDamage;
    } 
}
