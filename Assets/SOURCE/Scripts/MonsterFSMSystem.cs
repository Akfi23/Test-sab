using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterFSMSystem : GameSystem
{
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
            if(!monster.isDie)
                monster.FSMCOmponent.Work();
        }

        foreach(var monster in game.EnemyMonsters)
        {
            if (!monster.isDie)
                monster.FSMCOmponent.Work();
        }
    }
}
