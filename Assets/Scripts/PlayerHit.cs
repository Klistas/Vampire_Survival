using System.Collections;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [Header("피격관련")]
    // 최대 체력
    public float MaxHP;
    // 피격 색
    public Color HitColor;
    // 피격 시간
    public float HitTime;
    // 대미지(추후 변경될 수 있다)
    public float HitDamage;

    // 현재 체력
    private float currentHP;
    // 렌더러
    private SpriteRenderer sr;
    // 원래색
    private Color originColor;
    // 현재 피격 후 무적 상태인지
    private bool isInvincible;

    private void Start()
    {
        // 초기화
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
        currentHP = MaxHP;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌한 물체의 태그가 Enemy라면
        if (collision.collider.CompareTag("Enemy"))
        {
            if (isInvincible) return;
            // 대미지를 입는다.
            TakeDamage(HitDamage);
        }
    }

    /// <summary>
    /// 대미지를 입고, 체력이 0이하면 죽는 로직
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        // 무적 상태일때는 맞지않게끔 해주는게 먼저
        if (isInvincible)
            return;

        // 대미지 입히는거
        currentHP -= damage;

        // 무적루틴
        StartCoroutine(InvicibleRoutine());

        // 체력이 0 이하라면
        if (currentHP <= 0)
        {
            // 죽음 로직
            Die();
        }
    }

    /// <summary>
    /// 몇초간 무적 시간을 만들어주는 로직
    /// </summary>
    /// <returns></returns>
    private IEnumerator InvicibleRoutine()
    {
        // 피격 되었다는 사실을 알려줌.
        isInvincible = true;

        // 깜빡거리도록 만들어 볼 예정
        float elapsedTime = 0f;
        // while 문은 괄호안의 조건이 false가 되면 종료되므로, elapsedTime이 정해진 시간보다 커지면(지나면) 끝남.
        while (elapsedTime < HitTime)
        {
            sr.color = HitColor;
            yield return new WaitForSeconds(0.05f);
            sr.color = originColor;
            yield return new WaitForSeconds(0.05f);
            elapsedTime += 0.1f;
        }

        // 원래 색으로 변경
        sr.color = originColor;

        // 피격 후 무적 상태가 끝남을 알림.
        isInvincible = false;
    }

    // 죽는 로직
    public void Die()
    {
        // 현재는 시간만 멈춤. 추후 UI 등이 생성되면추가
        Time.timeScale = 0f;
    }

}
