using Cinemachine;
using System.Collections;
using UnityEngine;

public class FoodCanEvent : StageEvent
{
    [Header("��ȣ�ۿ뿡 ���� ī�޶� ��ȯ")]
    public CinemachineVirtualCamera cam;

    //�۵� ����
    private bool isWorking = false;

    //ķ�� ���� �켱����
    private int originalPriority;

    private void Start()
    {
        originalPriority = cam.Priority;
    }

    public override void DoEvent()
    {
        if(!isWorking)
        {
            isWorking = !isWorking;
            GameManager.Instance.Player.onDeath += StopBuff;
            ChangeCam(cam, 200, 10f);
        }
    }

    protected override IEnumerator ChangeCamPriority(CinemachineVirtualCamera cam, int priority, float delay)
    {
        cam.Priority = priority;

        GameManager.Instance.Player.playerController.moveSpeed += GameManager.Instance.Player.itemData.ExtraSpeed;
        GameManager.Instance.Player.playerController._animator.speed *= 3;

        yield return new WaitForSeconds(delay);

        cam.Priority = originalPriority;

        GameManager.Instance.Player.onDeath -= StopBuff;
        StopBuff();
    }

    //���� ���ֱ�
    public void StopBuff()
    {
        cam.Priority = originalPriority;
        GameManager.Instance.Player.playerController.moveSpeed -= GameManager.Instance.Player.itemData.ExtraSpeed;
        GameManager.Instance.Player.playerController._animator.speed /= 3;
        isWorking = false;
        StopAllCoroutines();
    }
}
