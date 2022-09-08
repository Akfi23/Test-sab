using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNewMonsterSystem : GameSystemWithScreen<MergeScreen>
{
    [SerializeField] private ParticleSystem bornVFX;

    public override void OnInit()
    {
        screen.BuyMeleeButton.Button.onClick.AddListener(() => BuyNewMonster(AttackType.Melee,screen.BuyMeleeButton));
        screen.BuyRangeButton.Button.onClick.AddListener(() => BuyNewMonster(AttackType.Range,screen.BuyRangeButton));
    }

    public override void OnStateEnter()
    {
        SetButtonPrice();
    }

    private void BuyNewMonster(AttackType type,BuyMonsterPanelComponent buyButton)
    {
        if (buyButton.Price <= player.Money)
        {
            if (buyButton.isTouched == false)
            {
                buyButton.isTouched = true;

                CellComponent cell;

                cell = FindEmptyCell();

                MonsterComponent newPlayerMonster;

                if (cell == null)
                { 
                    buyButton.isTouched=false;
                    screen.ShowNoEmptyCellText();
                    return; 
                }

                if (type == AttackType.Melee)
                {
                    newPlayerMonster = Instantiate(config.Melee[0], cell.transform.position, config.Melee[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
                    player.MeleePurchasedCount++;
                }
                else
                {
                    newPlayerMonster = Instantiate(config.Range[0], cell.transform.position, config.Range[0].transform.rotation * Quaternion.Euler(0, 180, 0), game.BattleField.transform);
                    player.RangePurchasedCount++;
                }

                bornVFX.transform.position = newPlayerMonster.transform.position;
                bornVFX.Play();
                newPlayerMonster.SetCell(cell);
                newPlayerMonster.SetOwner(Owner.Player);

                player.Money -= buyButton.Price;
                screen.UpdateMoneyText(player.Money);
                SetButtonPrice();

                buyButton.transform.DOScale(Vector3.one*1.1f,0.1f).SetEase(Ease.OutQuad).SetLoops(2,LoopType.Yoyo).OnComplete(()=>buyButton.isTouched=false);
                game.PlayerMonsters.Add(newPlayerMonster);
            }
        }
        else
        {
            if (buyButton.isTouched == false)
            {
                buyButton.isTouched = true;
                buyButton.transform.rotation = Quaternion.Euler(Vector3.zero);
                buyButton.transform.DOPunchRotation(Vector3.forward * 15f, 0.55f, 10, 1).SetEase(Ease.OutQuad).OnComplete(()=>buyButton.isTouched=false);
            }
        }
    }

    private CellComponent FindEmptyCell()
    {
        foreach (var cell in game.Cells)
        {
            if(cell.CellOwner==Owner.Player && cell.IsStaying == false)
            {
                return cell;
            }
        }

        return null;
    }

    private void SetButtonPrice()
    {
        if (player.MeleePurchasedCount < 1)
        {
            screen.BuyMeleeButton.UpdateStats(10, player.Money);
        }
        else
        {
            float currentPrice=10f;

            for (int i = 1; i < player.MeleePurchasedCount+1; i++)
            {
                currentPrice *= 1.2f;
            }

            currentPrice = Mathf.RoundToInt(currentPrice);

            if (currentPrice > 1000)
                currentPrice = 1000;

            screen.BuyMeleeButton.UpdateStats((int)currentPrice, player.Money);
        }

        if (player.RangePurchasedCount < 1)
        {
            screen.BuyRangeButton.UpdateStats(10, player.Money);
        }
        else
        {
            float currentPrice = 10f;

            for (int i = 1; i < player.RangePurchasedCount + 1; i++)
            {
                currentPrice *= 1.2f;
            }

            currentPrice = Mathf.RoundToInt(currentPrice);

            if (currentPrice > 1000)
                currentPrice = 1000;

            screen.BuyRangeButton.UpdateStats((int)currentPrice, player.Money);
        }
    }
}
