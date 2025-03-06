using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("움직임 관련")]
    //움직임 속도
    public float moveSpeed;
    //현재 입력되는 방향
    public Vector2 curMovementInput;
    //회전 속도
    public float rotateSpeed;

    public CinemachineBrain mainCam;

    [Header("점프")]
    public float jumpPower;

    [Header("애니메이션")]
    private Animator _animator;

    [Header("바닥 레이어")]
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
        //여긴 카메라의 정면을 기준으로 벡터값을 받아냄
        Vector3 cameraForward = mainCam.transform.forward;
        cameraForward.y = 0f; // Y축 영향을 제거
        cameraForward.Normalize();

        Vector3 cameraRight = mainCam.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 dir = cameraForward * curMovementInput.y + cameraRight * curMovementInput.x;

        //이동 방향이 바뀔 때만 회전 적용???? Slerp lerp 차이 없음??
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
        //땅에 닿았는지의 여부 추가
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
        //Ray 비교 상하좌우
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
