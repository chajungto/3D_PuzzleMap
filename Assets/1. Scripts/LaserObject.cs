using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour
{
    [Header("�� ����")]
    public float lineLength = 10f;

    [Header("���ط�")]
    public int damage = 50;

    [Header("�÷��̾� ���̾�")]
    public LayerMask playerLayerMask;

    //�۵� ����
    private bool isWorking = false;

    private float coolDown = 0f;

    private float coolTime = 3f;

    void Update()
    {
        Ray ray = new Ray(transform.position, - transform.right * lineLength);
        Debug.DrawRay(transform.position, - transform.right * lineLength, Color.red);

        if (Physics.Raycast(ray, lineLength, playerLayerMask) && !isWorking)
        {
            isWorking = true;
            GameManager.Instance.Player.TakeDamage(damage);
        }
        else
        {
            coolDown += Time.deltaTime;
            if(coolDown >= coolTime)
            {
                isWorking = false;
                coolDown = 0;
            }
        }
    }

}
