using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MergeScreenSystem : GameSystemWithScreen<MergeScreen>
{
    public override void OnStateEnter()
    {
        screen.FightButton.gameObject.SetActive(true);
        ScaleWithEnable(new Vector3(0.42f,0.48f, 0.38f), true, Ease.InCubic, 0.5f);
    }

    public override void OnInit()
    {
        screen.FightButton.onClick.AddListener(StartBattle);
        screen.UpdateMoneyText(player.Money);
    }

    private void StartBattle()
    {
        if (game.SelectedMonster != null) return;

        if (game.PlayerMonsters.Count > 0)
        {
            screen.FightButton.gameObject.SetActive(false);
            ScaleWithEnable(0.08f, false, Ease.Linear, 0);

            game.BattleField.FightZone.gameObject.SetActive(true);
            game.BattleField.FightZone.transform.DOScale(new Vector3(1.5f, 1.7f, 1.5f), 0.3f).SetEase(Ease.Linear);
            Bootstrap.Instance.ChangeGameState(GameStateID.Battle);
        }
        else
        {
            Debug.Log("EMPTY Player Team");
        }
    }

    private void ScaleWithEnable(float scaleFactor, bool status, Ease ease, float delayTime)
    {
        foreach (var cell in game.Cells)
        {
            cell.transform.DOKill();
            cell.transform.DOScale(Vector3.one * scaleFactor, 0.3f).SetEase(ease).OnComplete(() => cell.gameObject.SetActive(status)).SetDelay(delayTime);
        }
    }

    private void ScaleWithEnable(Vector3 scale, bool status, Ease ease, float delayTime)
    {
        foreach (var cell in game.Cells)
        {
            cell.transform.DOKill();
            cell.transform.DOScale(scale, 0.3f).SetEase(ease).OnComplete(() => cell.gameObject.SetActive(status)).SetDelay(delayTime);
        }
    }
}
