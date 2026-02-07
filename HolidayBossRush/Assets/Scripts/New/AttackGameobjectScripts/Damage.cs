using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField, Range(0, 5)] int damage;
    [SerializeField] private bool OnCollionDestroy;

    private newPlayerHealth _playerHealth;
    
    private bool _cantDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_cantDamage)
        {
            _playerHealth = other.GetComponent<newPlayerHealth>();
            _playerHealth.Damage(damage, transform.position);
            if(OnCollionDestroy == true)
            {
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Ground") && OnCollionDestroy)
        {
            Destroy(gameObject);
        }
    }

    public void CantDamage(bool canDamage) {
        _cantDamage = canDamage;
    } 
}
