using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorageSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Get<OnAppQuitSignal>().AddListener(SaveDataBeforeQuit);
    }

    private void SaveDataBeforeQuit()
    {
        SaveMonsterData();
        Bootstrap.Instance.SaveGame();
    }

    //public override void OnGameEnd()
    //{
    //    SaveMonsterData();
    //}

    private void SaveMonsterData()
    {
        if (game.PlayerMonsters.Count > 0)
        {
            player.MonsterDatas.Clear();

            foreach (var monster in game.PlayerMonsters)
            {
                var newData = new MonsterData(monster.AttackType, monster.EvolveIndex, monster.Cell.Index);
                player.MonsterDatas.Add(newData);
            }
        }
    }
}
