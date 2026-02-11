using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 어떤 프리팹을 생성할 건지.
    [Header("스폰대상")]
    public GameObject[] Enemies; // 생성할 적의 프리팹
    // 어느위치에서(랜덤생성 될건데 간격이나, 플레이어의 어떤반경 밖에서 생성될건지)
    [Header("스폰위치")]
    public Transform Player; // 플레이어의 반경계산을 위해 필요.
    public float SpawnDistance; // 플레이어와의 생성 거리
    // 몇초단위로 생성이 될건지
    [Header("스폰시간")]
    public float SpawnTime; // 기본적인 스폰 시간 간격

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    /// <summary>
    /// 정해진 시간 만큼 대기하고 몬스터를 생성
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEnemyRoutine()
    {
        while(true)
        {
            //어떤 위치에서 생성될지 연산
            Vector2 randomDirection = Random.insideUnitCircle.normalized; // 특정한 범위 내의 랜덤 위치를 찍어줌.
            Vector2 spawnOffset = randomDirection * SpawnDistance; // 찍어준 좁은 범위를 정해진 크기만큼 넓혀준다.
            Vector3 spawnPosition = Player.position + (Vector3)spawnOffset; // 플레이어의 위치를 중심으로 오프셋을 더해줌으로써 랜덤한 위치에 생성.
            //연산된 위치에 해당 프리팹을 생성
            PoolManager.instance.Get(0, spawnPosition);
            // SpawnTime 만큼 기다렸다가 생성.
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    /// <summary>
    /// 생성 범위를 확인하기 위함
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Player.position, SpawnDistance);
    }

}
