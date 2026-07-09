using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour
{
    [SerializeField] private float thrust;

    [SerializeField] 

    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(transform.up * thrust * Time.deltaTime * 100);
    }

    private void Update()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);
    }
}
    