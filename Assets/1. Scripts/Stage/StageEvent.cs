using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageEvent : MonoBehaviour
{
    [Header("��ȣ�ۿ뿡 ���� ī�޶� ��ȯ")]
    public CinemachineVirtualCamera stage01Cam;
    public CinemachineVirtualCamera stage02Cam;
    public CinemachineVirtualCamera stage03Cam;

    [Header("������ ť��")]
    public GameObject fallenCube;

    [Header("������ ť��")]
    public GameObject appearCube;

    [Header("���� ����")]
    public List<DirectionStool> directionStoolList;

    [Header("�Էµ� ������ ����")]
    public List<string> inputOrderDirectionList;

    //����
    List<string> answerList = new List<string>{ "S", "N", "O", "W" };

    public void Stage01Event()
    {
        ChangeCam(stage01Cam, 200, 2);
    }

    public void Stage02Event()
    {
        ChangeCam(stage02Cam, 200, 10);
    }

    public void Stage03Event()
    {
        CheckDirection();
    }




    public void FallCube()
    {
        fallenCube.AddComponent<Rigidbody>();
        StartCoroutine(RemoveCube(3f));
    }

    public void MakeCube()
    {
        StartCoroutine(AppearCube(1f));
    }

    private IEnumerator RemoveCube(float delay)
    {
        yield return new WaitForSeconds(delay);
        fallenCube.SetActive(false);
    }

    private IEnumerator AppearCube(float delay)
    {
        yield return new WaitForSeconds(delay);
        appearCube.SetActive(true);
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

        if (cam == stage01Cam)
        {
            FallCube();
        }
        else if(cam == stage03Cam)
        {
            MakeCube();
        }

        yield return new WaitForSeconds(delay * 0.5f);

        cam.Priority = originalPriority;
    }

    public void AddDirection(string direction)
    {
        inputOrderDirectionList.Add(direction);
    }

    public void CheckDirection()
    {
        if(inputOrderDirectionList.SequenceEqual(answerList))
        {
            Debug.Log("����");
            StartCoroutine(ChangeCamPriority(stage03Cam, 200, 3));
        }
        else
        {
            Debug.Log("����");
        }
        inputOrderDirectionList.Clear();
    }
}
