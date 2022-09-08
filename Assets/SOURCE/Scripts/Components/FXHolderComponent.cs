using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXHolderComponent : MonoBehaviour
{
    [SerializeField] private ParticleSystem runFX;

    public ParticleSystem RunFX => runFX;
}
