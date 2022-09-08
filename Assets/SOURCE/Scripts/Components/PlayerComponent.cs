using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerComponent : MonoBehaviour
{
    private NavMeshAgent agent;
    private AnimatorComponent animator;
    private TriggerComponent trigger;
    private FXHolderComponent fx;
    private PlayerHUDComponent playerHUD;
    private Collider col;
    [SerializeField] private Transform arrow;

    public NavMeshAgent Agent => agent;
    public AnimatorComponent Animator => animator;
    public TriggerComponent Trigger => trigger;
    public FXHolderComponent FX => fx;
    public PlayerHUDComponent PlayerHUD => playerHUD;
    public Collider Collider => col;
    public Transform Arrow => arrow;

    public void Init()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<AnimatorComponent>();
        animator.InitAnimator();

        trigger = GetComponent<TriggerComponent>();
        trigger.InitTrigger();

        fx = GetComponent<FXHolderComponent>();

        playerHUD = GetComponent<PlayerHUDComponent>();
        playerHUD.InitPlayerHUD();

        col = GetComponent<Collider>();
    }
}
