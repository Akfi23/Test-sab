using Cinemachine;
using Kuhpik;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoadingSystem : GameSystem
{
    [Tag] [SerializeField] private string gameCameraTag;

    public override void OnInit()
    {
        game.GameCamera = GameObject.FindGameObjectWithTag(gameCameraTag).GetComponent<CinemachineVirtualCamera>();
    }
}
