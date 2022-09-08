using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleInitSystem : GameSystem
{
    public override void OnInit()
    {
        //FindBattleMonsters();
    }

    private void FindBattleMonsters()
    {
        var monsters = game.BattleField.GetComponentsInChildren<MonsterComponent>().ToList();
        game.PlayerMonsters.Clear();
        game.EnemyMonsters.Clear();

        foreach (var monster in monsters)
        {
            monster.LevelText.gameObject.SetActive(false);

            if (monster.Owner == Owner.Player)
            {
                game.PlayerMonsters.Add(monster);
                PrepareMonsterToBattle(monster);
            }
            else
            {
                game.EnemyMonsters.Add(monster);
                PrepareMonsterToBattle(monster);
            }
        }
    }

    private void PrepareMonsterToBattle(MonsterComponent monsterComponent)
    {
        monsterComponent.FSMCOmponent.Init(game);
        monsterComponent.RenewStats();
        monsterComponent.FSMCOmponent.SetState(StateType.FindTarget);

        if(monsterComponent.Owner==Owner.Player)
            monsterComponent.HPBar.color = config.PlayerHPColor;
    }
}
