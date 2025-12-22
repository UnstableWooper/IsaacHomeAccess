using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad: MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;

    public float Power;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            rb.velocity = new Vector2(0, Power);
        }
    }
}
