using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GPSEvent : StageEvent
{
    [Header("��ȣ�ۿ뿡 ���� ī�޶� ��ȯ")]
    public CinemachineVirtualCamera cam;

    [Header("������ ť��")]
    public GameObject appearCube;

    [Header("�Էµ� ������ ����")]
    public List<string> inputOrderDirectionList;

    //������(South, North, O(����� �� ���), West)
    List<string> answerList = new List<string> { "S", "N", "O", "W" };

    private int eventCount = 0;

    public override void DoEvent()
    {
        if (eventCount == 0)
        {
            CheckDirection();
        }
    }

    public void MakeCube()
    {
        StartCoroutine(AppearCube(1f));
    }

    private IEnumerator AppearCube(float delay)
    {
        yield return new WaitForSeconds(delay);
        appearCube.SetActive(true);
    }

    protected override IEnumerator ChangeCamPriority(CinemachineVirtualCamera cam, int priority, float delay)
    {
        int originalPriority = cam.Priority;
        cam.Priority = priority;

        yield return new WaitForSeconds(delay * 0.5f);
        MakeCube();
        yield return new WaitForSeconds(delay * 0.5f);

        cam.Priority = originalPriority;
    }

    //������ ���� ����Ʈ�� �ֱ�
    public void AddDirection(string direction)
    {
        inputOrderDirectionList.Add(direction);
    }

    //���� Ȯ�� �� �ʱ�ȭ
    public void CheckDirection()
    {
        if (inputOrderDirectionList.SequenceEqual(answerList))
        {
            StartCoroutine(ChangeCamPriority(cam, 200, 3));
            eventCount++;
        }
        inputOrderDirectionList.Clear();
    }
}
