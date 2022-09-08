using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "CharacterState/Attack", order = 51)]
public class AttackState : CharacterState
{
    public override void OnStateEnter(MonsterComponent monster)
    {
        if(monster.Agent.enabled)
            monster.Agent.ResetPath();

        monster.Animator.SetBool("IsMove", false);
        monster.SetAttackStatusFalse();
        monster.timer = 0;
    }

    public override void OnStateExit(MonsterComponent monster)
    {
    }

    public override void Work(MonsterComponent monster)
    {
        if (monster.TargetMonster == null)
        {
            monster.FSMCOmponent.SetState(StateType.FindTarget);
        }
        else
        {
            monster.transform.LookAt(monster.TargetMonster.transform);
            monster.timer += Time.deltaTime;

            if (monster.timer >= 0.7f)
            {
                monster.Attack();
                monster.timer = 0;
            }

            if (monster.TargetMonster.isDie)
            {
                monster.ClearTarget();
                monster.FSMCOmponent.SetState(StateType.FindTarget);
            }

            if (monster.TargetMonster != null)
            {
                if (Vector3.Distance(monster.transform.position, monster.TargetMonster.transform.position) > monster.AttackRange + 1f)
                {
                    monster.FSMCOmponent.SetState(StateType.Chase);
                }
            }
        }

    }
}
