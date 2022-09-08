using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorComponent : MonoBehaviour
{
    private Animator animator;
    public Animator Animator => animator;

    private int DieHash = Animator.StringToHash("IsDie");
    private int AttackHash = Animator.StringToHash("IsAttack");
    private int MoveHash = Animator.StringToHash("IsMoving");
    public void InitAnimator()
    {
        animator = GetComponent<Animator>();
    }
    
    public void SetMoveState(bool status)
    {
        animator.SetBool(MoveHash, status);
    }

    public void SetDieState(bool status)
    {
        if (status)
            animator.SetTrigger(DieHash);
        else
            animator.ResetTrigger(DieHash);
    }

    public void SetAttackState(bool status)
    {
        if (status)
            animator.SetTrigger(AttackHash);
        else
            animator.ResetTrigger(AttackHash);
    }
}
