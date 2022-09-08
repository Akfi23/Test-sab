using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeInitSystem : GameSystemWithScreen<CartoonMaskScreen>
{
    public override void OnStateEnter()
    {
        TryGetMonstersBySave();
        CreateEnemyMonsters();

        screen.ZoomOutMask();
        game.BattleField.FightZone.gameObject.SetActive(false);
        game.BattleField.FightZone.localScale = Vector3.one * 0.35f;
    }

    private void TryGetMonstersBySave()
    {
        if (game.PlayerMonsters.Count <= 0)
        {
            if (player.MonsterDatas.Count > 0)
            {
                CreatePlayerMonstersBySave();
            }
            else
            {
                MonsterComponent newPlayerMonsterM = Instantiate(config.Melee[0], game.PlayerCells[0].transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
                MonsterComponent newPlayerMonsterR = Instantiate(config.Range[0], game.PlayerCells[1].transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
                newPlayerMonsterM.SetOwner(Owner.Player);
                newPlayerMonsterM.SetCell(game.PlayerCells[0]);
                newPlayerMonsterR.SetOwner(Owner.Player);
                newPlayerMonsterR.SetCell(game.PlayerCells[1]);

                game.PlayerMonsters.Add(newPlayerMonsterR);
                game.PlayerMonsters.Add(newPlayerMonsterM);
            }
        }
    }

    private void CreatePlayerMonstersBySave()
    {
        foreach (var monster in player.MonsterDatas)
        {
            MonsterComponent newPlayerMonster;
            CellComponent cell = game.PlayerCells.Find(x => x.Index == monster.Cell);

            if (monster.Type == AttackType.Melee)
            {
                newPlayerMonster = Instantiate(config.Melee[monster.EvolveIndex], cell.transform.position, config.Range[0].transform.rotation, game.BattleField.transform);
            }
            else
            {
                newPlayerMonster = Instantiate(config.Range[monster.EvolveIndex], cell.transform.position, config.Range[0].transform.rotation, game.BattleField.transform);
            }

            newPlayerMonster.SetOwner(Owner.Player);
            newPlayerMonster.SetCell(cell);
            newPlayerMonster.Agent.Warp(cell.transform.position);
            game.PlayerMonsters.Add(newPlayerMonster);
        }

        player.MonsterDatas.Clear();
    }

    private void CreateEnemyMonsters()
    {
        MonsterComponent newMonster;

        int counter = Random.Range(1, 2);

        for (int i = 0; i < counter; i++)
        {
            int evolveIndex = counter + Random.Range(0, 1);
            CellComponent cell = game.EnemyCells[i];

            if (i <= counter / 2)
                newMonster = Instantiate(config.Range[evolveIndex], cell.transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
            else
                newMonster = Instantiate(config.Melee[evolveIndex], cell.transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);


            newMonster.SetOwner(Owner.Enemy);
            newMonster.SetCell(cell);
        }
    }
}
