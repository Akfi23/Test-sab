using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeInitSystem : GameSystemWithScreen<CartoonMaskScreen>
{
    [SerializeField] private List<CellComponent> playerCells = new List<CellComponent>();
    [SerializeField] private List<CellComponent> enemyCells = new List<CellComponent>();

    public override void OnStateEnter()
    {
        if (game.PlayerMonsters.Count <= 0)
        {
            if (player.MonsterDatas.Count > 0)
            {
                CreatePlayerMonstersBySave();
            }
            else
            {
                MonsterComponent newPlayerMonsterM = Instantiate(config.Melee[0], playerCells[0].transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
                MonsterComponent newPlayerMonsterR = Instantiate(config.Range[0], playerCells[1].transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
                newPlayerMonsterM.SetOwner(Owner.Player);
                newPlayerMonsterM.SetCell(playerCells[0]);
                newPlayerMonsterR.SetOwner(Owner.Player);
                newPlayerMonsterR.SetCell(playerCells[1]);

                game.PlayerMonsters.Add(newPlayerMonsterR);
                game.PlayerMonsters.Add(newPlayerMonsterM);

            }
        }
        else
        {
            foreach (var monster in game.PlayerMonsters)
            {
                monster.transform.position = monster.Cell.transform.position;
                monster.gameObject.SetActive(true);
                monster.LevelText.gameObject.SetActive(true);
            }
        }

        screen.ZoomOutMask();
        game.BattleField.FightZone.gameObject.SetActive(false);
        game.BattleField.FightZone.localScale = Vector3.one * 0.35f;
    }

    public override void OnInit()
    {
        foreach (var cell in game.Cells)
        {
            if (cell.CellOwner == Owner.Enemy)
            {
                enemyCells.Add(cell);
            }
            else
            {
                playerCells.Add(cell);
            }
        }
    }

    private void CreatePlayerMonstersBySave()
    {
        int i = 0;

        foreach (var monster in player.MonsterDatas)
        {
            MonsterComponent newPlayerMonster;
            CellComponent cell = game.Cells.Find(x => x.Index == monster.Cell);

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
            i++;
        }

        player.MonsterDatas.Clear();
    }

    private void CreateMonsters(Owner owner)
    {
        MonsterComponent newMonster;


        for (int i = 0; i < 2; i++)
        {
            if (i <= 2 / 2)
            {
                CellComponent cell = enemyCells[i];
                newMonster = Instantiate(config.Range[1], cell.transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
                newMonster.SetOwner(Owner.Enemy);
                newMonster.SetCell(cell);
            }
            else
            {
                CellComponent cell = enemyCells[i];
                newMonster = Instantiate(config.Melee[1], cell.transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
                newMonster.SetOwner(Owner.Enemy);
                newMonster.SetCell(cell);
            }
        }

    }
}
