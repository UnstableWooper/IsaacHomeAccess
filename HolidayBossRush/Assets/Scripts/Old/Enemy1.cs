using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float EnemyHealthPoints = 6;

    public int AttackDamage;

    public GameObject player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        AttackDamage = FindAnyObjectByType<PlayerItems>().attackDamage;
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
