using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charging : MonoBehaviour
{
    // Start is called before the first frame update
    private int chargeLevel = 0;  // 차징 레벨 (0~2)
    private int currentBounce = 0;  // 현재 튕긴 횟수

    // 각 차징 단계별 튕길 수 있는 횟수
    private int[] bounceLimits = { 1, 2, 3 };  // 1단계, 2단계, 3단계의 튕길 수 있는 횟수

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // MultiMovement에서 차징 레벨 전달받음
    public void SetChargeLevel(int level)
    {
        chargeLevel = Mathf.Clamp(level, 0, bounceLimits.Length - 1);  // 0~2로 제한
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 차징 레벨에 따라 튕길 수 있는 횟수 제한
            if (currentBounce >= bounceLimits[chargeLevel])
            {
                Destroy(gameObject);  // 최대 횟수 넘기면 오브젝트 삭제
            }
            else
            {
                // 충돌 시 속도 계산
                Vector3 incomingVelocity = rb.velocity;

                // 벽의 법선 벡터를 기준으로 반사 벡터 계산
                Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, collision.contacts[0].normal);

                // 새로운 반사 속도 적용
                rb.velocity = reflectedVelocity;

                // 튕긴 횟수 증가
                currentBounce += 1;
            }
        }

        
    }

    /*
    public int CountCharge()
    {
        
    }
    */
}
