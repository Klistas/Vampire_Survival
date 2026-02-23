using UnityEngine;
using System.Collections.Generic;

public class OrbitalObject : MonoBehaviour
{
    // 대미지
    public float Damage;
    // 대미지를 입는 쿨타임
    public float HitCooltime;

    // 적 별로 얼마나 쿨타임인지 확인하는 자료구조
    private Dictionary<int, float> hitTimer = new Dictionary<int, float>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 지금 충돌중이라면 쿨타임을 확인해서 대미지를 입히는 로직
            int enemyId = collision.GetInstanceID();
            // 이전에 대미지를 입었을때의 시간과 현재시간의 차를 통해 쿨타임을 확인.
            float currentTime = Time.time;

            // 충돌한 물체의 아이디가 이미 존재하는지 확인
            if (hitTimer.ContainsKey(enemyId))
            {
                // 있으면 쿨타임을 확인해서, 쿨타임보다 두 시간의 차가 작을때 여기에서 스탑
                if (currentTime - hitTimer[enemyId] < HitCooltime)
                    return;

            }

            // 첫 충돌이거나 혹은 쿨타임이 지났다면
            hitTimer[enemyId] = currentTime;

            // 대미지와 넉백처리를 진행하면된다.
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<EnemyAI>().TakeHit(knockbackDir);
            // 데미지 주는 로직
            collision.GetComponent<EnemyHit>().TakeDamage(Damage);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 자료구조에서 뺀다
        if (collision.CompareTag("Enemy"))
        {
            // 충돌체의 아이디를 이용해 해당 물체를 제거.
            hitTimer.Remove(collision.GetInstanceID());
        }
    }
}
