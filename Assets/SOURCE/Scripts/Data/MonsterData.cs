using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData
{
    [SerializeField] private AttackType type;
    [SerializeField] private int evolveIndex;
    [SerializeField] private int cell;

    public AttackType Type => type;
    public int EvolveIndex => evolveIndex;
    public int Cell => cell;

    public MonsterData(AttackType type,int index,int cell)
    {
        this.type = type;
        evolveIndex = index;
        this.cell = cell;
    }
}
