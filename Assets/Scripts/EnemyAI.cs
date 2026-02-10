using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // 이동에 관련된 정보들(이동속도,플레이어,리지드바디...)
    [Header("이동 관련값")]
    public float MoveSpeed; // 이동 속도
    public Transform Player; // 플레이어의 위치

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start()
    {
        //초기화
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 플레이어와 자기자신의 방향을 연산 방향 = 플레이어의 위치 - 내 위치
        Vector3 direction = (Player.position - transform.position).normalized;
        // 연산한 방향으로 이동.
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
        // 이동할때, 방향에 따라서 Flip 방향전환
        if(direction.x > 0)
        {
            sr.flipX = false;
        }
        else if(direction.x < 0)
        {
            sr.flipX = true;
        }
    }

}
