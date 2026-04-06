using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEgg : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite[] sprites;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }else if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }   

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.sprite = sprites[Random.Range(0 ,sprites.Length)];

    }
}
