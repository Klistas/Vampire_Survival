using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private float lifeTime;
    private float damage;

    // 시간이 지남에 따라서 비활성화 되는 거.
    private void OnEnable()
    {
        StartCoroutine(DisableRoutine());
    }

    private IEnumerator DisableRoutine()
    {
        // 코루틴을 사용하여 시작부터 정해진 수명만큼 대기한다.
        yield return new WaitForSeconds(lifeTime);
        // 시간이 지나면 풀에 반납.
        PoolManager.instance.Return(gameObject, 1);
    }

    // 발사하면서 대미지나 스피드를 초기화 해주는 함수
    public void Init(float speed, float lifeTime, float damage)
    {
        // 매개변수와 Bullet 클래스의 멤버변수가 이름이 같으므로 this를 통해 현재 스크립트에 있는 멤버변수임을 명시적으로 알려줘야한다.
        // 위에 쓴건 this, 매개변수는 그냥쓰기
        this.speed = speed;
        this.lifeTime = lifeTime;
        this.damage = damage;
    }

    private void Update()
    {
        // 총알 움직임 로직
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌했을때 데미지를주고 비활성화.
        if(collision.CompareTag("Enemy"))
        {
            // 데미지 주는 로직
            collision.GetComponent<EnemyHit>().TakeDamage(damage);

            // 총알을 비활성화하고 반환
            PoolManager.instance.Return(gameObject, 1);
        }
        
    }
}
