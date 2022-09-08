using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Clear();
        Application.targetFrameRate = 120;
        game.BattleField = FindObjectOfType<BattleFieldComponent>();
        game.Cells = FindObjectsOfType<CellComponent>().ToList();

        for (int i = 0; i < game.Cells.Count; i++)
        {
            game.Cells[i].SetIndex((i + 1));
        }
    }
}
