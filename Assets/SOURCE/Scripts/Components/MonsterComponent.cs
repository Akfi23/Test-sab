using DG.Tweening;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum AttackType
{
    Melee,
    Range
}

public class MonsterComponent : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int currentHealth;
    [Header("CurrentOwner")]
    [SerializeField] private Owner owner;
    [Header("Components")]
    [SerializeField] private MonsterConfig config;
    [SerializeField] private Collider coll;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private MonsterFSMComponent fsmComponent;
    [SerializeField] private AnimatorComponent animator;
    [SerializeField] private HUDComponent hudComponent;
    [SerializeField] private FXHolderComponent fxHolder;
    [Header("AttackStuff")]
    [SerializeField] private MonsterComponent targetMonster;
    [SerializeField] private Transform projectile;
    [SerializeField] private bool isAttack;
    [SerializeField] private CellComponent cell;

    public bool isDie;

    public float timer;
    public MonsterConfig Config => config;
    public Owner Owner => owner;
    public Collider Collider => coll;
    public NavMeshAgent Agent => agent;
    public MonsterComponent TargetMonster => targetMonster;
    public MonsterFSMComponent FSMCOmponent => fsmComponent;
    public Transform Projectile => projectile;
    public bool IsAttack => isAttack;
    public AnimatorComponent Animator => animator;
    public CellComponent Cell => cell;
    public HUDComponent HUDComponent => hudComponent;

    public void Init()
    {
        coll = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        fsmComponent = GetComponent<MonsterFSMComponent>();
        animator = GetComponent<AnimatorComponent>();
        animator.InitAnimator();
        fxHolder = GetComponent<FXHolderComponent>();
        hudComponent = GetComponent<HUDComponent>();

        currentHealth = config.MaxHealth;
    }

    public void SetAttackStatusFalse()
    {
        isAttack = false;
    }

    public void SetOwner(Owner type)
    {
        owner = type;
    }

    public void SetCell(CellComponent cell)
    {
        if (this.cell != null)
        {
            this.cell.SetCellStatus(false);
        }

        this.cell = cell;
        this.cell.SetCellStatus(true);
    }

    public void SetTarget(MonsterComponent target)
    {
        targetMonster = target;    
    }

    public void ClearTarget()
    {
        targetMonster = null;
    }

    public void AttackOpponent()
    {
        if (targetMonster != null)
        {
            targetMonster.TakeDamage(config.Damage,this);
            isAttack = false;
        }
    }

    public void TakeDamage(int damage,MonsterComponent attacker)
    {
        if (isDie) return;

        fxHolder.HitFX.Play();
        hudComponent.StartHUDRoutine(config.MaxHealth,currentHealth);
        currentHealth-=damage;

        if (currentHealth <= 0)
        {
            Die(attacker);
        }
    }

    private void Die(MonsterComponent killer)
    {
        if (isDie) return;

        if (projectile != null)
        {
            projectile.transform.DOKill();
            projectile.gameObject.SetActive(false);
        }

        if(agent.enabled)
            agent.ResetPath();

        fxHolder.DeathFX.Play();
        hudComponent.HUD.gameObject.SetActive(false);
        Signals.Get<OnMonsterDie>().Dispatch(this,killer);
        animator.SetDieState();
        fsmComponent.SetState(StateType.Idle);
    }

    public void Attack()
    {
        if (isAttack) return;

        isAttack = true;

        if (config.AttackType == AttackType.Range)
        {
            projectile.transform.localPosition = Vector3.zero;

            animator.SetAttackState(true);
            projectile.gameObject.SetActive(true);

            projectile.DOJump(targetMonster.transform.position+Vector3.up*1f,2,1,0.65f).OnComplete(()=>
            {
                projectile.gameObject.SetActive(false);
                AttackOpponent();
            });
        }
        else
        {
            animator.SetAttackState(true);
        }
    }
}
