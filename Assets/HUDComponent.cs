using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDComponent : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text levelText;

    public Canvas HUD => canvas;
    public Image HPBar => hpBar;
    public TMP_Text LevelText => levelText;

    public void StartHUDRoutine(int max,int current)
    {
        StartCoroutine(HPBarRoutine(max, current));
    }

    IEnumerator HPBarRoutine(int maxHealth,int currentHealth)
    {
        canvas.gameObject.SetActive(true);
        hpBar.fillAmount = 1f / maxHealth * currentHealth;

        float t = Time.time;
        while (Time.time < t + 1)
        {
            canvas.transform.localRotation = Quaternion.Euler(-transform.localRotation.eulerAngles);
            yield return null;
        }

        canvas.gameObject.SetActive(false);
    }
}
