using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // 이동에 관련된 정보들(이동속도,플레이어,리지드바디...)
    [Header("이동 관련값")]
    public float MoveSpeed; // 이동 속도
    public float DisableDistance; // 얼마 멀어졌을때 비활성화 할 것인지
    public Transform Player; // 플레이어의 위치

    [Header("넉백 관련값")]
    public float KnockBackForce; // 뒤로 밀려날 힘
    public float KnockBackTime; // 몇초동안 밀려날지

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private bool isKnockBacked; // 밀려나고 있는지.

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

        // 플레이어와 멀어지면
        if(Vector2.Distance(transform.position,Player.position) > DisableDistance)
        {
            // 풀로 돌려줌
            PoolManager.instance.Return(gameObject, 0);
        }

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

    /// <summary>
    /// Bullet에서 호출하여 피격 반대 방향으로 밀어줄 것임.
    /// </summary>
    /// <param name="direction">피격 방향</param>
    public void TakeHit(Vector3 direction)
    {
        // 이미 밀려나고 있는 중이라면 더이상 밀려나지 않도록 할 것.
        if (isKnockBacked)
            return;

        // 특정 방향으로 리지드바디로 힘을 가할거고, 그동안은 움직임이 없도록 할 것임.
        StartCoroutine(KnockBackRoutine(direction));  

    }

    /// <summary>
    /// 전체적인 넉백 로직이 있는 코루틴
    /// </summary>
    /// <param name="direction"> 넉백 될 방향</param>
    /// <returns></returns>
    private IEnumerator KnockBackRoutine(Vector3 direction)
    {
        // 넉백중임을 알리고
        isKnockBacked = true;
        // 초기화해주고(리지드바디)
        rb.linearVelocity = Vector2.zero;

        // 방향을 받아서 힘을 가해주고
        rb.AddForce(direction * KnockBackForce, ForceMode2D.Impulse);

        // 특정시간만큼 기다렸다가
        yield return new WaitForSeconds(KnockBackTime);

        // 다시 원래대로 돌아올것
        isKnockBacked = false;
        rb.linearVelocity = Vector2.zero;
    }

}
