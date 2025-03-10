using Cinemachine;
using System.Collections;
using UnityEngine;

public class StageEvent : MonoBehaviour
{
    public virtual void DoEvent() { }

    //코루틴 실행
    protected virtual void ChangeCam(CinemachineVirtualCamera cam, int priority, float delay)
    {
        StartCoroutine(ChangeCamPriority(cam, priority, delay));
    }

    //카메라를 delay동안 순위를 priority로 바꿈
    protected virtual IEnumerator ChangeCamPriority(CinemachineVirtualCamera cam, int priority, float delay)
    {
        int originalPriority = cam.Priority;
        cam.Priority = priority;

        yield return new WaitForSeconds(delay);

        cam.Priority = originalPriority;
    }

}
