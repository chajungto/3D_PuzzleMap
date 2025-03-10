using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("움직임 관련")]
    public float moveSpeed;             //움직임 속도
    public Vector2 curMovementInput;    //현재 입력되는 방향
    public float rotateSpeed;           //회전 속도

    [Header("점프")]
    private bool isJumping;             //점프 여부
    public float jumpPower;             //점프 힘

    [Header("애니메이션")]
    private Animator _animator;

    [Header("바닥 레이어")]
    public LayerMask groundLayerMask;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        //떨림 방지 
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        //움직일 수 있을때 체크해줌
        isGrounded();
        Move();
    }

    void Move()
    {
        //여긴 카메라의 정면을 기준으로 벡터값을 받아냄
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 dir = cameraForward * curMovementInput.y + cameraRight * curMovementInput.x;

        //회전
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
                _animator.SetBool("isGrounded", true);
                _animator.SetBool("isFalling", false);
                return true;
            }
        }
        //닿는 게 없다면 false 반환, 그전에 떨어지고 있는지 여부도 세팅
        _animator.SetBool("isGrounded", false);
        isJumping = true;

        //점프 중이면서 y속도가 -1 이하인 경우
        if ((isJumping && _rigidbody.velocity.y <= -1f))
        {
            _animator.SetBool("isFalling", true);
        }

        return false;
    }




}
