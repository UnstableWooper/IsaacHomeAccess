using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PhysicsMaterial2D _Material;

    public float Friction { get; private set; }
    public bool OnGround { get; private set; }

    private LayerMask _layerMask;

    private void Start()
    {
        _layerMask = LayerMask.GetMask("Ground");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Collision(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Collision(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Collision(other);
    }

    private void Collision(Collision2D other)
    {
        _Material = other.gameObject.GetComponent<Rigidbody2D>().sharedMaterial;
        if(_Material != null)
        {
            Friction = _Material.friction;
        }
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, _layerMask);
        if (hit){
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
    }
}
