using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterConfig : ScriptableObject
{
    [Header("Attributes")]
    [SerializeField] private int evolveIndex;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int currentHealth;
    [SerializeField] private float attackRange;
    [Header("Types")]
    [SerializeField] private AttackType attackType;
    [SerializeField] private Owner owner;
}
