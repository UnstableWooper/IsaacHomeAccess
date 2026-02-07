using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    [SerializeField] protected float attackWarnLength;
    [SerializeField, Range(0, 4)] protected int _attackDamage;

    public abstract void StartAttack();
    public abstract IEnumerator AttackWarn();
    //Roll attack
    //Stomp
    //Shoot
    
}
