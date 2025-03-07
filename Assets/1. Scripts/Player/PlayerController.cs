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
    //메인 캠 -> camera.Main으로 바꿀 예정
    public CinemachineBrain mainCam;

    [Header("점프")]
    private bool isJumping;
    public float jumpPower;

    [Header("애니메이션")]
    private Animator _animator;

    [Header("바닥 레이어")]
    public LayerMask groundLayerMask;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        //커서 없애기
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        //떨림 방지 
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        isGrounded();

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
        //이동
        if (curMovementInput != Vector2.zero)
        {
            Vector3 newVector = targetRotation * Vector3.forward * moveSpeed;
            newVector.y = _rigidbody.velocity.y;
            _rigidbody.velocity = newVector;
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
            //_rigidbody.AddForce(Vector2.right, ForceMode.Impulse);
            _animator.SetTrigger("isJumping");
            isJumping = true;
        }

    }

    public void ForcedJump()
    {
        _rigidbody.AddForce(Vector2.up * jumpPower * 2, ForceMode.Impulse);
        _animator.SetTrigger("isJumping");
        isJumping = true;
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
            //닿는 게 있다면 true 반환, 그전 세팅도 함께
            if (Physics.Raycast(rays[i], 0.05f, groundLayerMask))
            {
                isJumping = false;
                //_animator.SetBool("isJumping", false);
                _animator.SetBool("isGrounded", true);
                _animator.SetBool("isFalling", false);
                return true;
            }
        }
        //닿는 게 없다면 false 반환, 그전에 떨어지고 있는지 여부도 세팅
        _animator.SetBool("isGrounded", false);
        isJumping = true;

        //점프 중이면서 y속도가 0 미만인 경우????????
        if ((isJumping && _rigidbody.velocity.y <= -1f))
        {
            _animator.SetBool("isFalling", true);
        }

        return false;
    }


}
