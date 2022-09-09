using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Container", menuName = "MonsterConfig/Config", order = 51)]
public class MonsterConfig : ScriptableObject
{
    [Header("Attributes")]
    [SerializeField] private int evolveIndex;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [Header("Types")]
    [SerializeField] private AttackType attackType;

    public int EvolveIndex => evolveIndex;
    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public float AttackRange => attackRange;
    public AttackType AttackType => attackType;
}
