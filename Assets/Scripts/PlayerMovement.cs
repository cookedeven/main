using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool IsHit = false;

    public bool IsPlayer = true;

    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private float dashTime = 0f;
    private bool isDashing = false;
    private Vector3 dashDirection;

    public float cooldownTime = 1f;
    private float cooldownTimer = 0f;

    private Rigidbody rb;

    public float projectileSpeed = 15f;
    public float chargeStage = 0.7f;  // 차징 단계1 기준
    public float maxChargeTime = 3f;  // 최대 차징 시간
    private float chargeTimer = 0f;
    private bool isCharging = false;

    public Transform handTransform;
    public GameObject heldObject;
    private Rigidbody heldRb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!IsPlayer)
        {
            return;
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // 대쉬 입력
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && cooldownTimer <= 0f)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashDuration)
            {
                EndDash();
            }
        }
        else
        {
            RotateTowardsMouse();
        }

        // 오브젝트 잡기 및 차징
        if (heldObject != null)
        {
            if (Input.GetMouseButton(0))  // 차징 시작
            {
                if (!isCharging)
                {
                    chargeTimer = 0f;  // 차징 타이머 리셋
                    isCharging = true;
                }
                chargeTimer += Time.deltaTime;
            }

            // 손에 오브젝트를 계속 위치시키기
            heldObject.transform.position = handTransform.position;
            heldObject.transform.rotation = handTransform.rotation;

            // 마우스 버튼을 떼면 오브젝트 던지기
            if (isCharging && Input.GetMouseButtonUp(0))
            {
                isCharging = false;
                ThrowHeldObject();
            }
        }
    }

    void FixedUpdate()
    {
        if (!IsPlayer) return;

        if (!isDashing)
        {
            Move();
        }
        else
        {
            Dash();
        }
    }

    // 이동
    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0f, moveZ).normalized;

        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    // 마우스를 향해 회전
    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0;
            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }

    // 대쉬 시작
    void StartDash()
    {
        isDashing = true;
        dashTime = 0f;
        cooldownTimer = cooldownTime;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(moveX, 0f, moveZ).normalized;

        dashDirection = inputDir != Vector3.zero ? inputDir : transform.forward;
    }

    // 대쉬
    void Dash()
    {
        rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
    }

    // 대쉬 종료
    void EndDash()
    {
        isDashing = false;
    }

    // 오브젝트 잡기
    public void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldRb = obj.GetComponent<Rigidbody>();

        if (heldRb != null)
        {
            heldRb.isKinematic = true;
        }

        obj.transform.SetParent(handTransform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }

    // 오브젝트 던지기
    void ThrowHeldObject()
    {
        // 차징에 따른 차징 레벨 계산
        int chargeLevel = 0;
        if (chargeTimer < 0.7f)
        {
            chargeLevel = 1;  // 약한 차징
        }
        else if (0.7f <= chargeTimer && chargeTimer < 1.4f)
        {
            chargeLevel = 2;  // 중간 차징
        }
        else
        {
            chargeLevel = 3;  // 강한 차징
        }

        // 던지기: 차징 레벨에 따라 속도 증가
        heldObject.transform.SetParent(null);
        if (heldRb != null)
        {
            heldRb.isKinematic = false;

            Vector3 flatForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

            // 차징 레벨에 따라 속도 조정
            heldRb.velocity = flatForward * (projectileSpeed + chargeLevel * 5f);
        }

        // 차징 상태 설정
        Charging charge = heldObject.GetComponent<Charging>();
        if (charge != null)
        {
            charge.SetChargeLevel(chargeLevel);
        }

        // 오브젝트 상태 업데이트
        Holdable holdable = heldObject.GetComponent<Holdable>();
        if (holdable != null)
        {
            holdable.IsHeld = false;
        }

        // 던진 후 heldObject 초기화
        heldObject = null;
        heldRb = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsHit) return;

        if (other.CompareTag("Holdable")) 
        {
            IsHit = true;
            Debug.Log("충돌!");


        }
    }
}
