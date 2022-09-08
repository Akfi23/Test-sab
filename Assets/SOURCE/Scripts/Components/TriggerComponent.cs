using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComponent : MonoBehaviour
{
    private Collider col;
    private OnTriggerCollideSignal collideSignal;
    public Collider Collider => col;

    public void InitTrigger()
    {
        col = GetComponent<Collider>();
        collideSignal = Signals.Get<OnTriggerCollideSignal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        collideSignal.Dispatch(other.transform, true);
    }

    private void OnTriggerExit(Collider other)
    {
        collideSignal.Dispatch(other.transform, false);
    }
}
