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

    [Header("����")]
    public float jumpPower;

    [Header("�ִϸ��̼�")]
    private Animator _animator;

    [Header("�ٴ� ���̾�")]
    public LayerMask groundLayerMask;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    void LateUpdate()
    {
        Move();
    }

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
            _rigidbody.velocity = targetRotation * Vector3.forward * moveSpeed;
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

    public void OnJump(InputAction.CallbackContext context)
    {
        //���� ��Ҵ����� ���� �߰�
        if (context.phase == InputActionPhase.Started && isGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            //_animator.SetTrigger("isJumping");
        }
        else
        {
            //_animator.SetBool("isJumping", false);
        }
    }

    bool isGrounded()
    {
        //Ray �� �����¿�
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }


}
