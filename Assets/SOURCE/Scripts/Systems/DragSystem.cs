using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSystem : GameSystem
{
    private Vector3 previousObjectPos;
    private OnMergeApprove mergeSignal;
    [SerializeField] private ParticleSystem landingFX;

    public override void OnInit()
    {
        mergeSignal = Signals.Get<OnMergeApprove>();
    }

    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray placeRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(placeRay, out hit, Mathf.Infinity) && hit.collider.TryGetComponent(out MonsterComponent monster))
            {
                SelectObject(monster);
            }
        }

        if (game.SelectedMonster != null)
            game.SelectedMonster.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray placeRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(placeRay, out hit, Mathf.Infinity, 1 << 6) && hit.collider.TryGetComponent(out MonsterComponent monster))
            {
                if (game.SelectedMonster != null)
                {
                    if (game.SelectedMonster.EvolveIndex == monster.EvolveIndex && game.SelectedMonster.AttackType==monster.AttackType)
                    {
                        if (game.SelectedMonster.EvolveIndex < config.Melee.Count - 1)
                        {
                            mergeSignal.Dispatch(monster);
                        }
                        else
                        {
                            UndoPos();
                        }
                    }
                    else
                    {
                        UndoPos();
                    }
                }
                else
                {
                    UndoPos();
                }
            }
            else if (Physics.Raycast(placeRay, out hit, Mathf.Infinity, 1 << 7) && hit.collider.TryGetComponent(out CellComponent cell))
            {
                if (game.SelectedMonster != null)
                {
                    if (cell.CellOwner == Owner.Player)
                    {
                        MoveToCell(cell);
                    }
                    else
                    {
                        UndoPos();
                    }
                }
            }
            else
            {
                UndoPos();
            }
        }
    }

    private void MoveToCell(CellComponent cell)
    {
        MonsterComponent tempObj = game.SelectedMonster;
        game.SelectedMonster.SetCell(cell);
        game.SelectedMonster = null;

        tempObj.transform.localScale = Vector3.one;
        tempObj.transform.DOMove(cell.transform.position, 0.15f).SetEase(Ease.Linear).OnComplete(() => { tempObj.transform.DOPunchScale(Vector3.one * -0.3f, 0.15f); landingFX.transform.position = tempObj.transform.position; landingFX.Play(); tempObj.Collider.enabled = true; });
    }

    private void UndoPos()
    {
        if (game.SelectedMonster != null)
        {
            MonsterComponent tempObj = game.SelectedMonster;

            game.SelectedMonster = null;
            tempObj.transform.localScale = Vector3.one;
            tempObj.transform.DOMove(previousObjectPos, 0.15f).SetEase(Ease.Linear).OnComplete(() => { tempObj.transform.DOPunchScale(Vector3.one * -0.3f, 0.15f); landingFX.transform.position = tempObj.transform.position; landingFX.Play();tempObj.Collider.enabled = true; });
        }
    }

    private void SelectObject(MonsterComponent monster)
    {
        if (monster.Owner == Owner.Player)
        {
            previousObjectPos = monster.transform.position;
            game.SelectedMonster = monster;
            game.SelectedMonster.transform.DOKill();
            game.SelectedMonster.Collider.enabled = false;
        }
    }
}
