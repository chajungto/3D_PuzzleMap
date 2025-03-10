using Cinemachine;
using UnityEngine;

public class MapEvent : StageEvent
{
    [Header("상호작용에 따른 카메라 전환")]
    public CinemachineVirtualCamera cam;

    public override void DoEvent()
    {
        ChangeCam(cam, 200, 10);
    }
}
