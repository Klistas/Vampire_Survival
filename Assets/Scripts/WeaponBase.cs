using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public Transform Player; // 플레이어의 위치
    public SkillData SkillData; // 자신의 스킬 데이터
    public int SkillLevel; // 자신의 스킬 레벨

    private float coolTimer; // 쿨타임 타이머

    /// <summary>
    /// SkillData를 사용해서 스킬을 초기화해주는 함수
    /// </summary>
    /// <param name="data"></param>
    /// <param name="level"></param>
    public virtual void Init(SkillData data, int level)
    {
        // 스킬데이터 적용
        this.SkillData = data;
        // 레벨 적용
        this.SkillLevel = level;
        // 플레이어 찾기(무기의 부모가 있는경우와 없는 경우로 나눠서 구현)
        if(transform.parent != null)
        {
            Player = transform.parent;
        }
        else
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            Player = obj.transform;
        }
    }

    private void Update()
    {
        CoolTimeAttack();
    }

    /// <summary>
    /// 쿨타임 타이머를 사용해서 시간마다 공격하게 해주는 함수
    /// </summary>
    private void CoolTimeAttack()
    {
        // 시간변화를 감지
        coolTimer += Time.deltaTime;
        // 시간변화가 해당 스킬데이터의 쿨타임보다 커지면
        if(coolTimer >= SkillData.cooldown)
        {
            // 공격하고
            Attack();
            // 쿨타임 초기화
            coolTimer = 0f;
        }
    }

    /// <summary>
    /// 레벨업 함수
    /// </summary>
    /// <param name="level"></param>
    public virtual void LevelUp(int level)
    {
        this.SkillLevel = level;
    }
    /// <summary>
    /// 투사체를 쏘는 시점에 플레이어와 가장 가까이 있는 적을 탐지
    /// </summary>
    /// <returns>적의 위치</returns>
    public Transform FindNearestEnemy()
    {
        // 플레이어 주변에 무형의 원을 그려서 탐지
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, SkillData.baseRange + SkillData.rangePerLevel);

        // 가장 가까운 적이 어떤 적인지 파악할수 있도록 할것임.

        Transform nearest = null; // 현재까지 가장 가까운 적
        float minDistance = Mathf.Infinity; // 현재까지 최소 거리

        foreach (Collider2D hit in hits)
        {
            // 태그가 Enemy 이고, 활성화 되어있는 적만 포함
            if (hit.CompareTag("Enemy") && hit.gameObject.activeSelf)
            {
                // 현재 감지된 적과 플레이어 사이의 거리
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                // 만약 종전까지의 최소거리보다 현재 탐지한 적의 거리가 더 작다면
                if (distance < minDistance)
                {
                    // 현재 거리를 최소거리로, 가까운 적을 현재 적으로 대입
                    minDistance = distance;
                    nearest = hit.transform;
                }
            }
        }

        return nearest;
    }
    // 공격 함수
    public abstract void Attack();
}
