using DG.Tweening;
using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeScreen : UIScreen
{
    public Button FightButton;
    public BuyMonsterPanelComponent BuyMeleeButton;
    public BuyMonsterPanelComponent BuyRangeButton;
    public TMP_Text NoEmptyCellsText;
    public TMP_Text MoneyCount;
    public void ShowNoEmptyCellText()
    {
        NoEmptyCellsText.gameObject.SetActive(true);
        NoEmptyCellsText.transform.DOKill();
        NoEmptyCellsText.transform.rotation = Quaternion.Euler(Vector3.zero);
        NoEmptyCellsText.transform.position = Input.mousePosition + Vector3.up * 200f;
        NoEmptyCellsText.transform.DOScale(Vector3.one * 1.25f, 0.15f);
        NoEmptyCellsText.transform.DORotate(new Vector3(0, 0, 15f), 0.35f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.InOutFlash);
        NoEmptyCellsText.transform.DOScale(Vector3.one * 0.15f, 0.15f).SetDelay(1.5f).OnComplete(() => NoEmptyCellsText.gameObject.SetActive(false));
    }

    public void UpdateMoneyText(int count)
    {
        MoneyCount.text = count.ToString();
    }
}
