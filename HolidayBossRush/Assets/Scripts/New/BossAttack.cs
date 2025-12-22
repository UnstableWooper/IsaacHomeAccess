using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    [SerializeField, Range(0, 4)] protected int _attackDamage;
    public abstract IEnumerator AttackWarn();
    public abstract void StartAttack();
    //Roll attack
    //Stomp
    //Shoot
    
}
