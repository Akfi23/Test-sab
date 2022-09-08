using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    Chase,
    Attack,
    FindTarget
}

public abstract class CharacterState : ScriptableObject
{
    //protected NoTransitionFSM<StateType, CharacterState> fsm;

    protected GameData gamedata;
    [SerializeField] protected StateType type;
    public StateType Type => type;

    public void Init(NoTransitionFSM<StateType, CharacterState> machine, GameData game)
    {
        //fsm = machine;
        gamedata = game;
    }

    public abstract void Work(MonsterComponent monster);

    public abstract void OnStateEnter(MonsterComponent monster);

    public abstract void OnStateExit(MonsterComponent monster);
   
}
