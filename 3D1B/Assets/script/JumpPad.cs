using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float bounceForce = 100f; // 튕겨 나갈 힘의 세기 (원하는 높이에 따라 조절)

    private void OnTriggerEnter(Collider other)
    {
        // 부딪힌 오브젝트가 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // 중요: 기존에 위나 아래로 움직이던 속도를 0으로 초기화 (이래야 항상 똑같은 높이로 뜁니다)
                playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);

                // 순간적인 충격(Impulse)으로 플레이어를 위로 튕겨냄
                playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
