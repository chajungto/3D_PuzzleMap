using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [Header("Ray ����")]
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    [Header("���� �����ִ� ������")]
    public GameObject curInteractGameObject;
    private Item curInteractable;

    [Header("������ ���� �����")]
    public Text promptTextName;
    public Text promptTextDescription;

    [Header("��ȣ�ۿ뿡 ���� ī�޶� ��ȯ")]
    public StageEvent stageEvent;

    [Header("��ȣ�ۿ뿡 ���� Action")]
    public Action stageAction;

    private void Start()
    {
        GameManager.Instance.Player.interactItem += InteractWithItem;
    }

    private void Update()
    {
        if (Time.time - checkRate > lastCheckTime)
        {
            lastCheckTime = Time.time;

            //�÷��̾��� �� ����
            Ray ray = new Ray(transform.position + transform.up, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.gameObject.GetComponent<Item>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptTextName.gameObject.SetActive(false);
                promptTextDescription.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptTextName.gameObject.SetActive(true);
        promptTextDescription.gameObject.SetActive(true);
        promptTextName.text = curInteractable.GetInteractItemName();
        promptTextDescription.text = curInteractable.GetInteractItemDescription();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();

            curInteractGameObject = null;
            curInteractable = null;

            promptTextName.gameObject.SetActive(false);
            promptTextDescription.gameObject.SetActive(false);
        }
    }

    public void InteractWithItem()
    {
        ItemData iData = curInteractable.data;
        switch (iData.Type)
        {
            case ItemType.Key:
                if (iData.IsCorrect)
                {
                    GameManager.Instance.Player.Heal(iData.ExtraHealth);
                    stageAction = stageEvent.Stage01Event;
                    stageAction.Invoke();
                }
                else
                {
                    GameManager.Instance.Player.TakeDamage(iData.ExtraHealth);
                }
                break;

            case ItemType.Map:
                stageAction = stageEvent.Stage02Event;
                stageAction.Invoke();
                break;

            case ItemType.GPS:
                stageAction = stageEvent.Stage03Event;
                stageAction.Invoke();
                break;

            default: return;
        }
    }


}
