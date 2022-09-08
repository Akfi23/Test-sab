using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OpponentComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 landPos;
    [SerializeField] private Collider coll;
    [SerializeField] private int level;
    [SerializeField] private bool isReady;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image timeFill;
    [SerializeField] private OpenableQuadComponent closedQuad;
    [SerializeField] private MonsterComponent[] monsters;
    private bool isDefeated;
    public Collider Collider => coll;
    public Animator Animator => animator;
    public int Level => level;
    public bool IsReady => isReady;
    public MonsterComponent[] Monsters => monsters;

    public void SetLandPos()
    {
        landPos = transform.position;
    }

    public void GoToLandPos(bool isWin)
    {
        transform.SetParent(null);
        transform.position = landPos;
        isReady = false;
        StartCoroutine(ColdDown(isWin));
    }

    private IEnumerator ColdDown(bool isWin)
    {
        float time = 0;

        if (!isDefeated)
        {
            isDefeated = true;
        }

        if (isWin)
        {
            time = 30f;

            if (closedQuad != null)
            {
                closedQuad.ActiveBool();
                closedQuad.TryGetQuadActive();
            }
        }
        else
        {
            time = 1f;
        }

        canvas.gameObject.SetActive(true);
        timeFill.DOFillAmount(1, time).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        isReady = true;
        canvas.gameObject.SetActive(false);
        timeFill.fillAmount = 0;
    }
}
