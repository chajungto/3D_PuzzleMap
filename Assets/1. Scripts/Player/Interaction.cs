using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [Header("Ray 관련")]
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    [Header("현재 보고있는 아이템")]
    public GameObject curInteractGameObject;
    private Item curInteractable;

    [Header("아이템 정보 설명란")]
    public Text promptTextName;
    public Text promptTextDescription;

    [Header("StageEvent")]
    public StageEvent stageEvent01;
    public StageEvent stageEvent02;
    public StageEvent stageEvent03;

    [Header("상호작용에 따른 Action")]
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

            //플레이어의 앞 기준
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
                    stageAction = stageEvent01.DoEvent;
                    stageAction.Invoke();
                }
                else
                {
                    GameManager.Instance.Player.TakeDamage(iData.ExtraHealth);
                }
                break;

            case ItemType.Map:
                stageAction = stageEvent02.DoEvent;
                stageAction.Invoke();
                break;

            case ItemType.GPS:
                stageAction = stageEvent03.DoEvent;
                stageAction.Invoke();
                break;

            default: return;
        }
    }


}
