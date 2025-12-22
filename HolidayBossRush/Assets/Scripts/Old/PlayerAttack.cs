using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject slash;

    public Vector3 direction;
    public int timeoutDestructor;

    public float attackCooldown;
    public float attackSpeed;
    public float attackLength;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && attackCooldown <= 0)
        {

            Attack();
        }
        attackCooldown -= Time.deltaTime;
    }

    public void Attack()
    {
        slash.SetActive(true);
        Invoke("ResetAttack", attackLength);
    }
    public void ResetAttack()
    {
        slash.SetActive(false);

        attackCooldown = attackSpeed;
    }
}
