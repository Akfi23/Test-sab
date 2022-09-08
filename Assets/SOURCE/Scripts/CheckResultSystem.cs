using DG.Tweening;
using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckResultSystem : GameSystemWithScreen<ResultScreen>
{
    private CartoonMaskScreen cartoonMask;

    public override void OnInit()
    {
        screen.ContinueButton.onClick.AddListener(QuitBattle);
        cartoonMask = UIManager.GetUIScreen<CartoonMaskScreen>();
    }

    public override void OnStateEnter()
    {
        SetBattleResult();
    }

    private void SetBattleResult()
    {
        screen.ContinueButton.interactable = true;

        if (game.isWin)
        {
            screen.ResultText.text = "WIN";
            screen.ResultPanel.color = screen.WinColor;
            screen.ResultLabel.sprite = screen.WinLabel;
        }
        else
        {
            screen.ResultText.text = "LOSE";
            screen.ResultPanel.color = screen.LoseColor;
            screen.ResultLabel.sprite = screen.LoseLabel;
        }

        screen.PrizeText.text = " + " + game.PrizeCount.ToString();
    }

    private void QuitBattle()
    {
        screen.ContinueButton.interactable = false;
        player.Money += game.PrizeCount;
        game.PrizeCount = 0;
        cartoonMask.ZoomInMask();
        StartCoroutine(QuitRoutine());
    }

    private IEnumerator QuitRoutine()
    {
        ClearEnemeyList();

        yield return new WaitForSeconds(0.8f);

        foreach (var monster in game.PlayerMonsters)
        {
            monster.RenewStats();
        }

        Bootstrap.Instance.ChangeGameState(GameStateID.Merge);
    }

    private void ClearEnemeyList()
    {
        if (game.EnemyMonsters.Count > 0)
        {
            foreach (var monster in game.PlayerMonsters)
            {
                monster.Agent.enabled = false;
                monster.transform.forward = Vector3.zero;
            }

            game.EnemyMonsters.Clear();
        }
    }
}
