using UnityEngine;

public class OrbitalWeapon : WeaponBase
{
    // 회전속도
    public float RotateSpeed;
    // 궤도를 회전하는 프리팹
    public GameObject OrbitalPrefab;

    // 생성된 프리팹
    private GameObject orbitalObject;
    // 현재 궤도상의 각도
    private float currentRotation;


    public override void Init(SkillData data, int level)
    {
        base.Init(data, level);
        if(orbitalObject == null)
        {
            Debug.Log("생성");
            //궤도 물체가 없을때, 등록되어있는 프리팹을 플레이어의 위치에 생성
            orbitalObject = Instantiate(OrbitalPrefab, Player.position, Quaternion.identity);
            OrbitalObject orbitObj = GetComponent<OrbitalObject>();
            if(orbitObj == null)
            {
                // 만약 해당 프리팹에 스크립트가 없는 경우 추가해줌
                orbitObj = orbitalObject.AddComponent<OrbitalObject>();
            }
            orbitObj.Damage = data.baseDamage;
            orbitObj.HitCooltime = data.cooldown;
        }
    }


    public override void LevelUp(int level)
    {
        base.LevelUp(level);
        // 기본적으로 Init에서 OrbitObject를 추가해주므로 가져오기만 하면됨
        OrbitalObject orbitObj = orbitalObject.GetComponent<OrbitalObject>();
        // 레벨업에 따른 대미지 상승
        orbitObj.Damage += SkillData.damagePerLevel;
    }

    private void LateUpdate()
    {
        // 회전 로직
        if (orbitalObject == null) return;

        // 각도를 누적시킴
        currentRotation += RotateSpeed * Time.deltaTime;

        // 원형의 궤도를 계산해야함
        float radius = SkillData.baseRange + SkillData.rangePerLevel;
        float rad = currentRotation * Mathf.Deg2Rad;

        // 원형 궤도를 위한 X,Y 값 계산 == 삼각함수로 진행(싸인 코사인)
        float x = Player.position.x + Mathf.Cos(rad) * radius;
        float y = Player.position.y + Mathf.Sin(rad) * radius;

        orbitalObject.transform.position = new Vector3(x, y, 0);
    }



    public override void Attack()
    {
    }
}
