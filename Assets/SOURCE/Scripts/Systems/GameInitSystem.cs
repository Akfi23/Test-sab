using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitSystem : GameSystemWithScreen<GameScreen>
{
    CartoonMaskScreen cartoonMask;
    public override void OnInit()
    {
        cartoonMask = UIManager.GetUIScreen<CartoonMaskScreen>();
    }

    public override void OnStateEnter()
    {
        cartoonMask.ZoomOutMask();
        screen.SetCurrentMoneyCount(player.Money);
        game.PrizeCount = 0;
    }
}
