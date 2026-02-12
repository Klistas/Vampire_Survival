using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [Header("적 설정값")]
    public float MaxHP; // 적의 최대 체력
    public Color HitColor; // 피격되었을때 깜빡거릴 색.
    public float FlashDuration; // 깜빡거리는 시간.

    //드랍 설정값

    private float currentHP; // 현재 체력
    private SpriteRenderer sp; // 색을 바꿔주기 위한 렌더러
    private Color originColor; // 원래 색.
    private bool isDead;

    private void Start()
    {
        // 초기화
        sp = GetComponent<SpriteRenderer>();
        originColor = sp.color;
    }

    private void OnEnable()
    {
        // 풀에서 가져와서 사용할때(활성화 될때) 풀피로 세팅한다.
        currentHP = MaxHP;
        isDead = false;
    }

    /// <summary>
    /// 적의 피격처리를 담당하는 함수. 데미지 적용 후 HP 0 이하면 사망(호출)
    /// </summary>
    /// <param name="damage">받은 데미지</param>
    public void TakeDamage(float damage)
    {
        // 이미 죽은 상황에서 또 충돌이 있을 가능성도 있다.
        // 이미 죽었을 경우에 예외처리
        if(isDead) return;

        // 데미지만큼 현재 체력을 깎아줌.
        currentHP -= damage;

        // 피격연출

        //현재 체력이 0 이하면
        if (currentHP <= 0)
        {
            //  사망
            Die();
        }
    }
    // 이미지의 색을 변경해서 피격되었을때 빨간색으로 깜빡이는 연출
    /// <summary>
    /// 죽는 기능 == HP를 잃는 기능 내부에 만약 HP가 0이하가 되면 호출. 적을 반환. 초기화. (보석,HP템).
    /// </summary>
    private void Die()
    {
        Debug.Log("죽음");
        // isDead = true 만들어서 죽음 상태로 만들고.
        isDead = true;
        // 풀에 반환한다.
        PoolManager.instance.Return(gameObject, 0);
    }
}
