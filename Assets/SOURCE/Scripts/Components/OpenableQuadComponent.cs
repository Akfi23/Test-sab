using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class OpenableQuadComponent : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject quadLand;
    [SerializeField] private NavMeshObstacle obstacle;
    [SerializeField] private int index;
    [SerializeField] private Collider quadTrigger;
    [SerializeField] private OpenableQuadComponent nextQuad;
    [SerializeField] private SpriteRenderer border;
    [SerializeField] private SpriteRenderer lockIcon;
    [SerializeField] private bool isClosed;

    private Vector3 tweenScale;
    private bool isTweened;

    public int Price => price;
    public TMP_Text PriceText => priceText;
    public int Index => index;
    public NavMeshObstacle Obstacle => obstacle;
    public Collider Collider => quadTrigger;
    public SpriteRenderer Border => border;
    public bool IsClosed => isClosed;
    public SpriteRenderer LockIcon => lockIcon;

    public void InitQuad(int index,int price,bool status)
    {
        tweenScale = quadLand.transform.localScale;
        quadLand.transform.localScale = Vector3.one * 0.3f;
        this.index = index;
        quadTrigger = GetComponent<Collider>();
        this.price = price;
        isClosed = status;

        TryGetQuadActive();

        if (price > 0)
        {
            priceText.text = price.ToString();
            LoadNextSquad(false);
        }
        else
        {
            OpenQuadLand();
        }
    }

    public void TryGetQuadActive()
    {
        if (isClosed)
        {
            quadTrigger.enabled = false;
            border.gameObject.SetActive(false);
            //lockIcon.gameObject.SetActive(true);
        }
        else
        {
            if (!isTweened)
            {
                isTweened = true;
                lockIcon.transform.DOScale(Vector3.one * 0.15f, 0.3f).OnComplete(() =>
                   {
                       quadTrigger.enabled = true;
                       border.transform.localScale = Vector3.one * 0.2f;

                       if (price > 0)
                       {
                            border.gameObject.SetActive(true);
                            border.transform.DOScale(Vector3.one, 0.3f);
                       }
                       else
                       {
                           border.gameObject.SetActive(false);
                       }

                       lockIcon.gameObject.SetActive(false);
                   });
            }
            else
            {

                if (price > 0)
                {
                    border.gameObject.SetActive(true);
                }
                else
                {
                    border.gameObject.SetActive(false);
                }

                quadTrigger.enabled = true;
                lockIcon.gameObject.SetActive(false);
            }
        }
    }

    public void ActiveBool()
    {
        isClosed = false;
    }

    public void OnBuyingQuad()
    {
        price--;
        priceText.text = price.ToString();
    }

    public void OnBuyingQuad(int price)
    {
        this.price = price;
        priceText.text = price.ToString();
    }

    public void OpenQuadLand()
    {
        quadLand.SetActive(true);
        quadLand.transform.DOScale(tweenScale, 0.5f).SetEase(Ease.InOutCubic);
        obstacle.gameObject.SetActive(false);
        quadTrigger.enabled = false;

        LoadNextSquad(true);
    }

    public void LoadNextSquad(bool status)
    {
        if (nextQuad != null)
        {
            nextQuad.PriceText.enabled = status;
            nextQuad.Border.enabled = status;

            if (price > 0)
            {
                if (nextQuad.IsClosed)
                {
                    nextQuad.Collider.enabled = false;
                    nextQuad.LockIcon.gameObject.SetActive(false);
                }
                else
                {
                    nextQuad.Collider.enabled = true;
                    nextQuad.LockIcon.gameObject.SetActive(false);
                }
            }
            else
            {
                if(nextQuad.IsClosed)
                    nextQuad.LockIcon.gameObject.SetActive(true);
                else
                    nextQuad.LockIcon.gameObject.SetActive(false);
            }
        }
    }
}
