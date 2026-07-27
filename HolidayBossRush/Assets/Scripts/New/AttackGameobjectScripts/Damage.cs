using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public bool onCollionDestroy;

    private newPlayerHealth _playerHealth;

    public bool canDamage;
    public bool collidingPlayer;

    public GameObject ThisGameObject { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHealth = other.gameObject.GetComponent<newPlayerHealth>();
            _playerHealth.GameObjectDamage = this;

            collidingPlayer = true;
        }

        if (other.CompareTag("Ground") && onCollionDestroy)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collidingPlayer = false;

            _playerHealth = null;
        }
    }

    public void CantDamage(bool cantDamage)
    {
        canDamage = !cantDamage;
    }
}
