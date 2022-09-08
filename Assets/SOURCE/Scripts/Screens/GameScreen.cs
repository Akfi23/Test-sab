using Kuhpik;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : UIScreen
{
    [Header("Money")]
    [HorizontalLine(color: EColor.Blue)]
    public Image MoneyIcon;
    public Transform MoneyPanel;
    public TMP_Text MoneyText;
    public Button Cheat;

    public void SetCurrentMoneyCount(int money)
    {
        MoneyText.text = money.ToString();
    }
}
