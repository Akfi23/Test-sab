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
    [Header("Attributes")]
    [SerializeField] private int evolveIndex;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int currentHealth;
    [Header("Types")]
    [SerializeField] private AttackType attackType;
    [SerializeField] private Owner owner;
    [Header("Components")]
    [SerializeField] private Collider coll;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private MonsterFSMComponent fsmComponent;
    [SerializeField] private Animator animator;
    [Header("AttackStuff")]
    [SerializeField] private MonsterComponent targetMonster;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform projectile;
    [SerializeField] private bool isAttack;
    [SerializeField] private CellComponent cell;
    [SerializeField] Canvas canvas;
    [SerializeField] Image hpBar;
    //[SerializeField] private ParticleSystem dieVFX;
    //[SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private TMP_Text levelText;

    public bool isDie;

    public float timer;
    public int EvolveIndex => evolveIndex;
    public int Health => health;
    public AttackType AttackType => attackType;
    public Owner Owner => owner;
    public Collider Collider => coll;
    public NavMeshAgent Agent => agent;
    public MonsterComponent TargetMonster => targetMonster;
    public MonsterFSMComponent FSMCOmponent => fsmComponent;
    public float AttackRange => attackRange;
    public Transform Projectile => projectile;
    public bool IsAttack => isAttack;
    public Animator Animator => animator;
    public int Damage => damage;
    public CellComponent Cell => cell;
    public Image HPBar => hpBar;
    public TMP_Text LevelText => levelText;

    public void Init()
    {
        
    }

    public void RenewStats()
    {
        currentHealth = health;
        FSMCOmponent.SetState(StateType.Idle);
        isDie = false;
        isAttack = false;
        targetMonster = null;

        if (projectile != null)
            projectile.gameObject.SetActive(false);
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
            targetMonster.TakeDamage(damage,this);
            isAttack = false;
        }
    }

    public void TakeDamage(int damage,MonsterComponent attacker)
    {
        if (isDie) return;

        //hitVFX.Play();
        StartCoroutine(HPBarRoutine());
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
            projectile.transform.DOKill();

        if(agent.enabled)
            agent.ResetPath();

        //dieVFX.Play();
        canvas.gameObject.SetActive(false);
        Signals.Get<OnMonsterDie>().Dispatch(this,killer);
        animator.SetTrigger("IsDie");
        fsmComponent.SetState(StateType.Idle);
    }

    IEnumerator HPBarRoutine()
    {
        canvas.gameObject.SetActive(true);
        hpBar.fillAmount = 1f / health * currentHealth;

        float t = Time.time;
        while (Time.time < t + 1) 
        {
            canvas.transform.localRotation = Quaternion.Euler(-transform.localRotation.eulerAngles);
            yield return null;
        }
        
        canvas.gameObject.SetActive(false);
    }

    public void Attack()
    {
        if (isAttack) return;

        isAttack = true;

        if (attackType == AttackType.Range)
        {
            projectile.transform.localPosition = Vector3.zero;

            animator.SetTrigger("IsAttack");
            projectile.gameObject.SetActive(true);

            projectile.DOJump(targetMonster.transform.position+Vector3.up*1f,2,1,0.65f).OnComplete(()=>
            {
                projectile.gameObject.SetActive(false);
                AttackOpponent();
            });
        }
        else
        {
            animator.SetTrigger("IsAttack");
        }
    }
}
