using DG.Tweening;
using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartoonMaskScreen : UIScreen
{
    public Transform CartoonMask;
    public void ZoomOutMask()
    {
       CartoonMask.transform.DOScale(Vector3.one * 8, 0.5f).SetEase(Ease.Linear);
    }

    public void ZoomInMask()
    {
       CartoonMask.transform.DOScale(Vector3.one * 0.2f, 0.75f).SetEase(Ease.Linear);
    }
}
