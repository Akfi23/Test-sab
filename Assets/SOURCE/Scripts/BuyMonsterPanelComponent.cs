using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyMonsterPanelComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private AttackType attackType;
    [SerializeField] private Image currentMonsterIcon;
    [SerializeField] private Button button;
    [SerializeField] private int price;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    public bool isTouched;

    public TMP_Text PriceText => priceText;
    public AttackType AttackType => attackType;
    public Image CurrentMonsterIcon => currentMonsterIcon;
    public Button Button => button;
    public int Price => price;

    public void UpdateStats(int price,int playerMoney)
    {
        priceText.text = price.ToString();
        this.price = price;

        SetButtonStatus(playerMoney);
    }

    public void SetButtonStatus(int playerMoney)
    {
        if (playerMoney>=this.price)
        {
            button.image.sprite=activeSprite;
        }
        else
        {
            button.image.sprite = inactiveSprite;
        }
    }
}
