using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSystem : GameSystem
{
    public override void OnInit()
    {
        //Signals.Get<OnMonsterDie>().AddListener(FindNewTarget);
    }

    public override void OnStateEnter()
    {
        foreach (var monster in game.PlayerMonsters)
        {
            monster.FSMCOmponent.SetState(StateType.Chase);
        }

        foreach (var monster in game.EnemyMonsters)
        {
            monster.FSMCOmponent.SetState(StateType.Chase);
        }
    }

    public override void OnUpdate()
    {
        foreach (var monster in game.PlayerMonsters)
        {
            if(monster.gameObject.activeInHierarchy)
                monster.FSMCOmponent.Work();
        }

        foreach(var monster in game.EnemyMonsters)
        {
            if (monster.gameObject.activeInHierarchy)
                monster.FSMCOmponent.Work();
        }
    }

    public void FindNewTarget(MonsterComponent victim , MonsterComponent killer)
    {
        killer.FSMCOmponent.SetState(StateType.FindTarget);
    }
}
