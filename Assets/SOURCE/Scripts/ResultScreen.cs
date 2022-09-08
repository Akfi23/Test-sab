using Kuhpik;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : UIScreen
{
    [Header("RESULT")]
    [HorizontalLine(color: EColor.Yellow)]
    public Transform Result;
    public Image ResultPanel;
    public Image ResultLabel;
    public Button ContinueButton;
    public TMP_Text PrizeText;
    public TMP_Text ResultText;
    public Color WinColor;
    public Color LoseColor;

    public Sprite WinLabel;
    public Sprite LoseLabel;
}
