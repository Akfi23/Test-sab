using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleProgerssionSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Get<OnMonsterDie>().AddListener(CheckBattleStatusAfterDie);
    }

    public override void OnStateEnter()
    {
        FindBattleMonsters();
        PrepareMonstersToFight();
    }

    private void FindBattleMonsters()
    {
        var monsters = game.BattleField.GetComponentsInChildren<MonsterComponent>().ToList();
        game.PlayerMonsters.Clear();

        foreach (var monster in monsters)
        {
            monster.LevelText.gameObject.SetActive(false);
            if (monster.Owner == Owner.Player)
            {
                game.PlayerMonsters.Add(monster);
                monster.FSMCOmponent.Init(game);
            }
            else
            {
                game.EnemyMonsters.Add(monster);
                monster.FSMCOmponent.Init(game);
            }
        }
    }

    private void PrepareMonstersToFight()
    {
        foreach (var monster in game.PlayerMonsters)
        {
            monster.Agent.enabled = true;
            monster.RenewStats();
            monster.HPBar.color = config.PlayerHPColor;
            monster.FSMCOmponent.SetState(StateType.FindTarget);
        }

        foreach (var monster in game.EnemyMonsters)
        {
            monster.Agent.enabled = true;
            monster.FSMCOmponent.SetState(StateType.FindTarget);
        }
    }

    private void CheckBattleStatusAfterDie(MonsterComponent victim, MonsterComponent killer)
    {
        StartCoroutine(OnMonsterDie(victim,killer));
    }

    private IEnumerator OnMonsterDie(MonsterComponent victim,MonsterComponent killer)
    {
        if(victim.isDie) yield break;

        victim.isDie = true;

        if (victim.Owner == Owner.Player)
        {
            yield return new WaitForSeconds(0f);
            victim.gameObject.SetActive(false);
        }
        else
        {
            game.PrizeCount += victim.Health;
            game.EnemyMonsters.Remove(victim);
        }

        if (game.EnemyMonsters.Count <= 0)
        {
            game.isWin = true;
            StartCoroutine(QuitBattleRoutine());
        }

        int i = 0;
        foreach (var monster in game.PlayerMonsters)
        {
            if(monster.isDie && monster.gameObject.activeInHierarchy == false)
            {
                i++;
            }
        }

        if(i>=game.PlayerMonsters.Count)
        {
            game.isWin = false;
            StartCoroutine(QuitBattleRoutine());
        }
    }

    private IEnumerator QuitBattleRoutine()
    {
        foreach (var monster in game.PlayerMonsters)
        {
            monster.FSMCOmponent.SetState(StateType.Idle);
            monster.Animator.SetBool("IsMove", false);
            monster.Agent.enabled = false;
        }

        yield return new WaitForSeconds(1f);

        Bootstrap.Instance.ChangeGameState(GameStateID.Result);
    }
}
