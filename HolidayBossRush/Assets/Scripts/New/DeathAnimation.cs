using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private float addForce;

    private Rigidbody2D _rigidbody2D;
    private Collider2D[] _colliders;

    public void Death()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(Vector2.up * addForce, ForceMode2D.Impulse);
        
        _colliders = GetComponents<Collider2D>();
        foreach (Collider2D singleCollider in _colliders)
        {
            singleCollider.enabled = false;
        }
        
        Invoke("Destroy", 3);
    }
    
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
