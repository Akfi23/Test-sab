using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSMComponent : MonoBehaviour
{
    private NoTransitionFSM<StateType, CharacterState> fsm;

    [SerializeField] private List<CharacterState> states;
    [SerializeField] private MonsterComponent monster;

    [SerializeField] private StateType initialState;
    [SerializeField] CharacterState currentState;

    public MonsterComponent Monster => monster;

    public void Init(GameData data)
    {
        monster = gameObject.GetComponent<MonsterComponent>();
        fsm = new NoTransitionFSM<StateType, CharacterState>();

        foreach (var state in states)
        {
            fsm.AddState(state.Type, state);
            state.Init(fsm, data);
        }

        //SetState(initialState);
    }

    public  StateType GetState()
    {
        return fsm.GetCurrentState();
    }

    public  void SetState(StateType stateType)
    {
        fsm.ChangeState(stateType);
        fsm.State.OnStateEnter(monster);
        //Debug.Log($"State <color=green>{fsm.State.name}</color> Enter");
        currentState = fsm.State;
    }

    //public void SetStateWithDelay(StateType type, float delay)
    //{
    //    fsm.ChangeState(type, delay);
    //    fsm.State.OnStateEnter(monster);
    //    //Debug.Log($"State <color=green>{fsm.State.name}</color> Enter");
    //    currentState = fsm.State;
    //}

    public void Work()
    {
        fsm.State.Work(monster);
    }

    public bool FindAvailableState(StateType type)
    {
        return fsm.FindState(type);
    }
}
