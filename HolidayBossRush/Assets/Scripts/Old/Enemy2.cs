using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //This is a patrolling enemy
    private RaycastHit2D groundCheckRight;
    private RaycastHit2D groundCheckLeft;

    public float speed;

    public Rigidbody2D rb;

    public float minX;
    public float maxX;
    public float EnemyHealthPoints = 8;
    public int AttackDamage;

    public Vector3 direction;
    void Start()
    {
        direction = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(rb.position, Vector2.down, Color.blue, 100000000f);
        //groundCheckRight = Physics2D.Raycast(rb.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
        
       
        //Vector3 velocity = rb.velocity;
        //velocity.x = + speed;
        //rb.velocity = velocity;

        AttackDamage = FindAnyObjectByType<PlayerItems>().attackDamage;
        if(transform.position.x < minX)
        {
            rb.velocity = new Vector3(0, 0, 0);
            direction = Vector3.right;
        }
        else if (transform.position.x > maxX)
        {
            rb.velocity = new Vector3(0, 0, 0);
            direction = Vector3.left;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            EnemyHealthPoints -= AttackDamage;

            if (EnemyHealthPoints <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
