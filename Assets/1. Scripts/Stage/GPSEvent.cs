using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GPSEvent : StageEvent
{
    [Header("상호작용에 따른 카메라 전환")]
    public CinemachineVirtualCamera cam;

    [Header("생성될 큐브")]
    public GameObject appearCube;

    [Header("입력될 발판의 방향")]
    public List<string> inputOrderDirectionList;

    //정답지(South, North, O(가운데가 원 모양), West)
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

    //제출할 방향 리스트에 넣기
    public void AddDirection(string direction)
    {
        inputOrderDirectionList.Add(direction);
    }

    //정답 확인 및 초기화
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
