using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEvent : MonoBehaviour
{
    [Header("상호작용에 따른 카메라 전환")]
    public CinemachineVirtualCamera stage01Cam;

    [Header("떨어질 큐브")]
    public GameObject fallenCube;

    public void Stage01Event()
    {
        ChangeCam(stage01Cam, 200, 2);
    }

    public void FallCube()
    {
        fallenCube.AddComponent<Rigidbody>();
        StartCoroutine(ChangeCamPriority(3f));
    }

    private IEnumerator ChangeCamPriority(float delay)
    {
        yield return new WaitForSeconds(delay);
        fallenCube.SetActive(false);
    }

    public void ChangeCam(CinemachineVirtualCamera cam, int priority, float delay)
    {
        StartCoroutine(ChangeCamPriority(cam, priority, delay));
    }

    private IEnumerator ChangeCamPriority(CinemachineVirtualCamera cam, int priority, float delay)
    {
        int originalPriority = cam.Priority;
        cam.Priority = priority;

        yield return new WaitForSeconds(delay * 0.5f);
        FallCube();
        yield return new WaitForSeconds(delay * 0.5f);

        cam.Priority = originalPriority;
    }

}
