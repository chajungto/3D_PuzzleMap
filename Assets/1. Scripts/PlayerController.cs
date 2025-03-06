using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("������ ����")]
    //������ �ӵ�
    public float moveSpeed;
    //���� �ԷµǴ� ����
    public Vector2 curMovementInput;
    //ȸ�� �ӵ�
    public float rotateSpeed;

    public CinemachineBrain mainCam;

    //[Header("Look")]
    //public Transform cameraContainer;
    //public float minXLook;
    //public float maxXLook;
    //private float camCurXRot;
    //public float lookSensitivity;
    //private Vector2 mouseDelta;
    //public bool canLook = true;


    [Header("�ִϸ��̼�")]
    private Animator _animator;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        Move();
    }

    //private void LateUpdate()
    //{
    //    if (canLook)
    //    {
    //        CameraLook();
    //    }
    //}

    void Move()
    {
        //���� ī�޶��� ������ �������� ���Ͱ��� �޾Ƴ�
        Vector3 cameraForward = mainCam.transform.forward;
        cameraForward.y = 0f; // Y�� ������ ����
        cameraForward.Normalize();

        Vector3 cameraRight = mainCam.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 dir = cameraForward * curMovementInput.y + cameraRight * curMovementInput.x;

        //�̵� ������ �ٲ� ���� ȸ�� ����???? Slerp lerp ���� ����??
        Quaternion targetRotation = transform.GetChild(0).rotation;
        if (dir != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(dir);
            transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }

        if (curMovementInput != Vector2.zero)
        {
            _rigidbody.velocity = targetRotation * Vector3.forward * moveSpeed * Time.deltaTime;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            _animator.SetBool("isWalking", true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            _animator.SetBool("isWalking", false);
        }

    }

    //void CameraLook()
    //{
    //    camCurXRot += mouseDelta.y * lookSensitivity;
    //    camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
    //    cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

    //    transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    //}

    //public void OnLook(InputAction.CallbackContext context)
    //{
    //    mouseDelta = context.ReadValue<Vector2>();
    //}

}
