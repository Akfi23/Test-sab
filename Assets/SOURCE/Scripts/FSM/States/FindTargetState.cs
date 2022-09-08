using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindTargetState", menuName = "CharacterState/FindTarget", order = 51)]
public class FindTargetState : CharacterState
{
    public override void OnStateEnter(MonsterComponent monster)
    {
        if (monster.TargetMonster != null)
        {
            if (monster.TargetMonster.isDie==false) monster.FSMCOmponent.SetState(StateType.Attack);
        }

        if (monster.Owner == Owner.Player)
        {
            if (gamedata.EnemyMonsters.Count > 0)
            {
                monster.SetTarget(GetClosestInCollectionFromTarget(monster, gamedata.EnemyMonsters));
            }
        }
        else
        {
            if (gamedata.PlayerMonsters.Count > 0)
            {
                monster.SetTarget(GetClosestInCollectionFromTarget(monster,gamedata.PlayerMonsters));
            }
        }


        if (monster.TargetMonster != null)
        {
            if (monster.TargetMonster.isDie == false)
            {
                monster.FSMCOmponent.SetState(StateType.Attack);
            }
        }
        else
        {
            monster.FSMCOmponent.SetState(StateType.Idle);
        }
    }

    public override void OnStateExit(MonsterComponent monster)
    {
    }

    public override void Work(MonsterComponent monster)
    {
    }

    public MonsterComponent GetClosestInCollectionFromTarget(MonsterComponent Target, List<MonsterComponent> List)
    {
        int index = 0;
        float min = Mathf.Infinity;
        for (int i = 0; i < List.Count; i++)
        {
            if (!List[i].isDie)
            {
                float dist = Vector3.Distance(List[i].transform.position, Target.transform.position);
                if (dist < min)
                {
                    index = i;
                    min = dist;
                }
            }
        }
        return List[index];
    }

}
