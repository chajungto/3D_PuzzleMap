using Cinemachine;
using UnityEngine;

public class MapEvent : StageEvent
{
    [Header("��ȣ�ۿ뿡 ���� ī�޶� ��ȯ")]
    public CinemachineVirtualCamera cam;

    public override void DoEvent()
    {
        ChangeCam(cam, 200, 10);
    }
}
