using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float stickyLength;
    [SerializeField] private PhysicsMaterial2D stickyPhaseMaterial;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private PhysicsMaterial2D _ogPhysicsMaterial;
    private Color _ogColor;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _ogPhysicsMaterial = _rigidbody.sharedMaterial;
        _ogColor = _spriteRenderer.color;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SlimeMode"))
        {
            StartCoroutine(StickyPhase());
        }
    }

    IEnumerator StickyPhase()
    {
        _rigidbody.sharedMaterial = stickyPhaseMaterial;
        _spriteRenderer.color = Color.Lerp(Color.yellow, Color.red, 0.1f);
        yield return new WaitForSeconds(stickyLength);
        _rigidbody.sharedMaterial = _ogPhysicsMaterial;
        _spriteRenderer.color = _ogColor;
    }
}
