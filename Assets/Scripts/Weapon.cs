using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 어떤 프리팹을 쏠건지
    // 사거리, 속도, 대미지....
    [Header("무기 설정값")]
    public int WeaponIndex; // 풀에있는 무기의 인덱스
    public float FireLate; // 발사 속도
    public float FireRange; // 사거리

    [Header("투사체 설정값")]
    public float Damage; // 대미지
    public float BulletSpeed;
    public float LifeTime;

    private Transform target; // 타겟의 위치

    // 현재 쏘는 타이밍에 플레이어와 가장 가까이 있는 적을 탐지하는 알고리즘

    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while(true)
        {
            // 타겟 찾기
            target = FindNearestEnemy();
            
            // 발사
            if(target != null)
                Shoot();

            // 대기
            yield return new WaitForSeconds(FireLate);
        }
    }

    /// <summary>
    /// 투사체를 쏘는 시점에 플레이어와 가장 가까이 있는 적을 탐지
    /// </summary>
    /// <returns>적의 위치</returns>
    private Transform FindNearestEnemy()
    {
        // 플레이어 주변에 무형의 원을 그려서 탐지
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, FireRange);

        // 가장 가까운 적이 어떤 적인지 파악할수 있도록 할것임.

        Transform nearest = null; // 현재까지 가장 가까운 적
        float minDistance = Mathf.Infinity; // 현재까지 최소 거리

        foreach(Collider2D hit in hits)
        {
            // 태그가 Enemy 이고, 활성화 되어있는 적만 포함
            if(hit.CompareTag("Enemy") && hit.gameObject.activeSelf)
            {
                // 현재 감지된 적과 플레이어 사이의 거리
                float distance = Vector2.Distance(transform.position,hit.transform.position);

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

    private void Shoot()
    {
        // 오브젝트 풀에서 가져오고,
        // 위치 잡아주고,
        GameObject bulletObj = PoolManager.instance.Get(WeaponIndex, transform.position);
        // 방향 잡아주고,
        Vector3 direction = target.position - transform.position;
        bulletObj.transform.up = direction;
        // 데미지 전달해주면 끝
        bulletObj.GetComponent<Bullet>().Init(BulletSpeed, LifeTime, Damage);
        // 사운드
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, FireRange);
    }
}
