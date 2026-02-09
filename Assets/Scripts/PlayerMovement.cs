using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed; // 이동 속도

    private Rigidbody2D rb; // 물리연산을 통해 이동할 것이므로 리지드바디 필요.
    private SpriteRenderer sp; // 플레이어 방향전환을 위한 렌더러.
    private Vector2 moveInput; // 키보드 입력값.

    private void Start()
    {
        // 초기화 작업 진행. 플레이어에 있는 리지드바디,스프라이트 렌더러 가져오기
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveInput();
        FlipPlayer();
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
    /// 플레이어의 방향에 따라 플레이어를 좌우 반전시키는 함수.
    /// </summary>
    private void FlipPlayer()
    {
        // 만약, 입력값이 오른쪽 = 양수, 왼쪽 = 음수
        if(moveInput.x > 0)
        {
            // 오른쪽을 향하고 있는 경우.
            sp.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            // 왼쪽을 향하고 있는 경우.
            sp.flipX = true;
        }
        // moveInput.x == 0 이면 따로 어디에도 안들어가고, 그대로 유지.
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
