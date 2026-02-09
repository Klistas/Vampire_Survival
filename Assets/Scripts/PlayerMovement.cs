using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed; // 이동 속도

    private Rigidbody2D rb; // 물리연산을 통해 이동할 것이므로 리지드바디 필요.
    private Vector2 moveInput; // 키보드 입력값.

    private void Start()
    {
        // 초기화 작업 진행. 플레이어에 있는 리지드바디 가져오기
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveInput();
    }

    private void FixedUpdate()
    {
        Move();
    }


    /// <summary>
    /// 입력값을 통해 플레이어의 이동 방향을 결정.
    /// </summary>
    private void MoveInput()
    {
        // 이미 매핑된 Horizontal,Vertical 키를 통해 입력값을 받을 수 있음.
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// 받은 입력값으로 직접적으로 이동하는 함수.
    /// </summary>
    private void Move()
    {
        // 이동할 위치 = 현재 위치 + 움직일 위치의 방향 * 얼마나 움직일지.
        Vector2 nextPos = rb.position + moveInput.normalized * MoveSpeed *Time.fixedDeltaTime;
        // Rigidbody의 포지션 자체를 이동시킴 + 충돌도 유지.
        rb.MovePosition(nextPos);
    }
}
