using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

namespace Kuhpik
{
    [CreateAssetMenu(menuName = "Config/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        // Example
        // [SerializeField] [BoxGroup("Moving")] private float moveSpeed;
        // public float MoveSpeed => moveSpeed;

        [SerializeField] private List<MonsterComponent> melee;
        [SerializeField] private List<MonsterComponent> range;
        [SerializeField] private int startMoney;
        [SerializeField] private Color playerHPColor;

        public List<MonsterComponent> Melee => melee;
        public List<MonsterComponent> Range => range;
        public int StartMoney => startMoney;
        public Color PlayerHPColor => playerHPColor;
    }
}