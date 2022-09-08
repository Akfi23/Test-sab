using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXHolderComponent : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathFX;
    [SerializeField] private ParticleSystem hitFX;

    public ParticleSystem DeathFX => deathFX;
    public ParticleSystem HitFX => hitFX;
}
