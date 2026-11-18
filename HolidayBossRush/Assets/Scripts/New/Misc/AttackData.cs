using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackData
{
    [SerializeField] public BossAttack attack;
    [SerializeField] public int randomChance;
}