using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Owner
{
    Player,
    Enemy
}

public class CellComponent : MonoBehaviour
{
    [SerializeField] private Owner cellOwner;
    [SerializeField] bool isStaying;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] private int index;
    public Owner CellOwner => cellOwner;
    public bool IsStaying => isStaying;
    public SpriteRenderer Sprite => sprite;
    public int Index => index;

    public void SetCellStatus(bool status) 
    {
        isStaying = status;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
}
