using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private float addForce;
    [SerializeField] private Sprite deathSprite;

    private Rigidbody2D _rigidbody2D;
    private Collider2D[] _colliders;
    
    [SerializeField] private Component[] components;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    public void Death()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.AddForce(Vector2.up * addForce, ForceMode2D.Impulse);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = deathSprite;
        
        _colliders = GetComponents<Collider2D>();
        foreach (Component component in components)
        {
            if (component is Behaviour componentBehaviour)
            {
                componentBehaviour.enabled = false;
            }
        }
        
        Invoke("Destroy", 5f);
    }
    
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
