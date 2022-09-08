using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSystem : GameSystem
{
    [SerializeField] private ParticleSystem mergeFX;

    public override void OnInit()
    {
        Signals.Get<OnMergeApprove>().AddListener(MergeByType);
    }

    private void MergeByType(MonsterComponent monster)
    {
        MonsterComponent evolvedMonster;

        monster.Cell.SetCellStatus(false);
        game.SelectedMonster.Cell.SetCellStatus(false);

        mergeFX.transform.position = monster.transform.position;
        mergeFX.Play();

        if (monster.AttackType == AttackType.Melee)
        {
            evolvedMonster = Instantiate(config.Melee[monster.EvolveIndex + 1],monster.transform.position, monster.transform.rotation, game.BattleField.transform);
        }
        else
        {
            evolvedMonster = Instantiate(config.Range[monster.EvolveIndex + 1], monster.transform.position, monster.transform.rotation, game.BattleField.transform);
        }

        if (game.PlayerMonsters.Count > 0)
        {
            game.PlayerMonsters.Remove(game.SelectedMonster);
            game.PlayerMonsters.Remove(monster);
            game.PlayerMonsters.Add(evolvedMonster);
        }

        evolvedMonster.transform.localScale = Vector3.one * 0.6f;
        evolvedMonster.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InCubic);
        evolvedMonster.transform.position = monster.transform.position;
        evolvedMonster.SetOwner(Owner.Player);
        evolvedMonster.SetCell(monster.Cell);

        Destroy(game.SelectedMonster.gameObject);
        Destroy(monster.gameObject);
    }
}
