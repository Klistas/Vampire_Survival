using UnityEngine;

public class ShotGun : WeaponBase
{
    public float SpreadAngle; // 총알이 발사될 각도
    public int BulletIndex; // 사용할 총알의 인덱스

    // 공격 함수(부모 클래스에서 구현하지 않았음)
    public override void Attack()
    {
        // 가까운 적을 찾을것임.
        Transform target = base.FindNearestEnemy();

        if (target == null) return;

        // 레벨에 맞춰서 투사체수가 많아 질 거라서 그거 계산
        int bulletCount = SkillData.baseProjectileCount + (SkillLevel - 1);

        // 적의 방향을 구함.
        Vector3 baseDirection = (target.position - Player.position).normalized; // 벡터의 차를 통해 해당 타겟으로의 방향을 구함. 이후 정규화
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg - 90f;

        float angleStep = 0f;
        // 각 투사체의 각도와 간격을 설정
        if(bulletCount > 1)
        {
            angleStep = SpreadAngle / (bulletCount - 1);
        }
        float startAngle = baseAngle - SpreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            // 총알 각각의 각도를 지정해줌
            float currentAngle = baseAngle;

            if (bulletCount > 1)
            {
                currentAngle = startAngle + angleStep * i;
            }

            // 풀에서 총알을 가져오고 위치와 각도를 초기화
            GameObject bullet = PoolManager.instance.Get(BulletIndex,Player.position);
            bullet.transform.rotation = Quaternion.Euler(0f,0f,currentAngle);

            // 총알에 속도와 대미지를 넣어준다.
            bullet.GetComponent<Bullet>().Init(SkillData.projectileSpeed, SkillData.lifeTime, SkillData.baseDamage + SkillData.damagePerLevel);
        }

    }
    // 초기화
    public override void Init(SkillData data, int level)
    {
        base.Init(data, level);
        BulletIndex = data.itemId;
    }
    
}
