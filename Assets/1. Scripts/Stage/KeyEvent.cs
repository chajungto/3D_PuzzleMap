using Cinemachine;
using System.Collections;
using UnityEngine;

public class KeyEvent : StageEvent
{
    [Header("��ȣ�ۿ뿡 ���� ī�޶� ��ȯ")]
    public CinemachineVirtualCamera cam;

    [Header("������ ť��")]
    public GameObject fallenCube;

    private int eventCount = 0;

    public override void DoEvent()
    {
        if(eventCount == 0)
        {
            ChangeCam(cam, 200, 2);
            eventCount++;
        }
    }

    //����߸� ť�꿡 �߷� �ο�
    public void FallCube()
    {
        fallenCube.AddComponent<Rigidbody>();
        StartCoroutine(RemoveCube(3f));
    }

    //3�ʵ� �ڵ����� �����
    private IEnumerator RemoveCube(float delay)
    {
        yield return new WaitForSeconds(delay);
        fallenCube.SetActive(false);
    }

    protected override IEnumerator ChangeCamPriority(CinemachineVirtualCamera cam, int priority, float delay)
    {
        int originalPriority = cam.Priority;
        cam.Priority = priority;

        yield return new WaitForSeconds(delay * 0.5f);

        FallCube();
        
        yield return new WaitForSeconds(delay * 0.5f);

        cam.Priority = originalPriority;
    }
}
