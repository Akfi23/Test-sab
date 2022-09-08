using DG.Tweening;
using Kuhpik;
using Supyrb;
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
        SetBattleResult();
    }

    private void SetBattleResult()
    {
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
        cartoonMask.ZoomInMask();
        StartCoroutine(QuitRoutine());
    }

    private IEnumerator QuitRoutine()
    {
        Signals.Get<OnAppQuitSignal>().Dispatch();
        yield return new WaitForSeconds(0.8f);
        Bootstrap.Instance.GameRestart(0);
    }
}
