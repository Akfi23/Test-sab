using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDComponent : MonoBehaviour
{
    private Canvas playerCanvas;
    [SerializeField] private Image startBattleFill;

    public Canvas PlayerCanvas => playerCanvas;
    public Image StartBattleFill => startBattleFill;

    public void InitPlayerHUD()
    {
        playerCanvas = GetComponentInChildren<Canvas>(true);
    }
}
