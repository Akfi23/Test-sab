using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // public float LevelProgress;
        // public Enemy[] Enemies;

        [Header("Cameras")]
        [HorizontalLine(color: EColor.Orange)]
        public CinemachineVirtualCamera GameCamera;

        [Header("Merge")]
        [HorizontalLine(color: EColor.Violet)]
        public MonsterComponent SelectedMonster;
        public List<CellComponent> Cells = new List<CellComponent>();
        public List<CellComponent> PlayerCells = new List<CellComponent>();
        public List<CellComponent> EnemyCells = new List<CellComponent>();

        [Header("Battle Stuff")]
        [HorizontalLine(color: EColor.Pink)]
        public bool isWin;
        public BattleFieldComponent BattleField;
        public List<MonsterComponent> PlayerMonsters = new List<MonsterComponent>();
        public List<MonsterComponent> EnemyMonsters = new List<MonsterComponent>();

        [Header("Result")]
        [HorizontalLine(color: EColor.Pink)]
        public int PrizeCount;
    }
}