using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldComponent : MonoBehaviour
{
    [SerializeField] private Transform fightZone;
    public Transform FightZone => fightZone;
}
