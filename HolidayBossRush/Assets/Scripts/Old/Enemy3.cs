using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float speed;
    public int takeDamage;

    public GameObject player;

    public Rigidbody2D rb;

    public float EnemyHealthPoints = 8;
    public int AttackDamage;

    public Vector3 direction;
    void Start()
    {
        direction = Vector3.left;

        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < player.transform.position.x)
        {
            direction = Vector3.right;
        }
        else if (transform.position.x > player.transform.position.x)
        {
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
