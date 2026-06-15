using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 및 점프 설정")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;
    public float fallMultiplier = 4f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 캐릭터가 물리 힘 때문에 넘어지지 않도록 회전 고정
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // 1. 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // 2. 물리 기반 이동 처리
        Move();

        // 3. 가속 낙하 (Fast Fall) 적용
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Move()
    {
        // 방향키 입력 받기
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // 메인 카메라 방향 기준으로 이동 방향 계산
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        Vector3 moveDir = (forward * z + right * x).normalized;

        // ★ 고친 부분: 현재 Y축 속도를 변수에 미리 담아두어 상승 힘이 중간에 깎이지 않도록 보존합니다.
        float currentY = rb.linearVelocity.y;

        // Rigidbody 속도만 변경하여 미끄러지지 않고 묵직하게 움직이게 만듭니다.
        Vector3 velocity = new Vector3(
            moveDir.x * moveSpeed,
            currentY,
            moveDir.z * moveSpeed
        );

        rb.linearVelocity = velocity;
    }

    // --- 충돌 및 플랫폼 탑승 처리 ---
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.SetParent(null);
            isGrounded = false;
        }
    }

    // ★ 여기 아래부터 코인 충돌 감지 로직을 새로 추가했습니다.
    private void OnTriggerEnter(Collider other)
    {
        // ★ 고친 부분: 유니티 에디터 태그 규칙에 맞게 소문자 coin을 대문자 Coin으로 변경했습니다.
        if (other.CompareTag("coin"))
        {
            // 코인 오브젝트를 삭제 (또는 점수 획득 로직을 여기에 연동 가능)
            Destroy(other.gameObject);
        }
    }
}
