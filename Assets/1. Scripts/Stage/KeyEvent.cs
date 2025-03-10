using Cinemachine;
using System.Collections;
using UnityEngine;

public class KeyEvent : StageEvent
{
    [Header("상호작용에 따른 카메라 전환")]
    public CinemachineVirtualCamera cam;

    [Header("떨어질 큐브")]
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

    //떨어뜨릴 큐브에 중력 부여
    public void FallCube()
    {
        fallenCube.AddComponent<Rigidbody>();
        StartCoroutine(RemoveCube(3f));
    }

    //3초뒤 자동으로 사라짐
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
