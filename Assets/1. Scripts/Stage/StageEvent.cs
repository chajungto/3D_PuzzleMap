using Cinemachine;
using System.Collections;
using UnityEngine;

public class StageEvent : MonoBehaviour
{
    public virtual void DoEvent() { }

    //�ڷ�ƾ ����
    protected virtual void ChangeCam(CinemachineVirtualCamera cam, int priority, float delay)
    {
        StartCoroutine(ChangeCamPriority(cam, priority, delay));
    }

    //ī�޶� delay���� ������ priority�� �ٲ�
    protected virtual IEnumerator ChangeCamPriority(CinemachineVirtualCamera cam, int priority, float delay)
    {
        int originalPriority = cam.Priority;
        cam.Priority = priority;

        yield return new WaitForSeconds(delay);

        cam.Priority = originalPriority;
    }

}
