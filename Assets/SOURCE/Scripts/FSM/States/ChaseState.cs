using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "CharacterState/Chase", order = 51)]
public class ChaseState : CharacterState
{
    public override void OnStateEnter(MonsterComponent monster)
    {
        if (monster.TargetMonster != null)
        {
            monster.Agent.SetDestination(monster.TargetMonster.transform.position);
            monster.Animator.SetMoveState(true);
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
        if (monster.TargetMonster != null)
        {
            monster.transform.LookAt(monster.TargetMonster.transform);

            if (Vector3.Distance(monster.transform.position, monster.TargetMonster.transform.position) < monster.Config.AttackRange)
            {
                monster.FSMCOmponent.SetState(StateType.Attack);
            }
            else
            {
                monster.FSMCOmponent.SetState(StateType.Chase);
            }
        }
        else
        {
            monster.FSMCOmponent.SetState(StateType.FindTarget);
        }

    }
}
