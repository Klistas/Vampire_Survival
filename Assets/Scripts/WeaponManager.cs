using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    // 현재 사용중인 무기와 무기의 레벨을 가지고 있을것.
    private Dictionary<string, WeaponBase> activeWeapons = new Dictionary<string, WeaponBase>(); // 내가 사용하고 있는 무기
    private Dictionary<string, int> activeWeaponsLevel = new Dictionary<string, int>(); // 내가 사용하고 있는 무기의 레벨

    public SkillData skilldata;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SelectSkill(skilldata);

        }
    }


    // 카드선택했을때의 로직을 만들것임. => 만약 내가 가지고 있는 스킬 == 레벨업, 가지고 있지 않으면 == 생성
    public void SelectSkill(SkillData data)
    {

        if(activeWeapons.ContainsKey(data.skillName))
        {
            // 최대 레벨이면 레벨안해줌
            int currentLevel = activeWeaponsLevel[data.skillName];

            if (currentLevel >= data.maxLevel)
                return;

            // 레벨 딕셔너리 수정
            activeWeaponsLevel[data.skillName] = currentLevel++;

            // 아니면 레벨업 함수 호출
            LevelUpSkill(data,activeWeapons[data.skillName], activeWeaponsLevel[data.skillName]);

        }
        else
        {
            // 생성하는 함수 호출, 레벨 딕셔너리 초기화
            activeWeaponsLevel[data.skillName] = 1;
            CreateSkill(data);
        }
    }

    // 레벨업하는 함수
    private void LevelUpSkill(SkillData data, WeaponBase weapon, int level)
    {
        switch (data.WeaponSkillType)
        {
            case SkillType.MultiShot:
                ShotGun shotgun = weapon as ShotGun;
                shotgun.LevelUp(level);
                break;
            case SkillType.Orbital:
                OrbitalWeapon orbit = weapon as OrbitalWeapon;
                orbit.LevelUp(level);
                break;
            case SkillType.Projectile:
                break;
        }
    }

    // 생성하는 함수
    private void CreateSkill(SkillData data)
    {
        // 플레이어위치에 프리팹을 생성
        GameObject skillPrefab = Instantiate(data.projectilePrefab, transform.position, Quaternion.identity);
        skillPrefab.transform.parent = transform;

        switch (data.WeaponSkillType)
        {
            case SkillType.MultiShot:
                ShotGun shotgun = skillPrefab.GetComponent<ShotGun>();
                // 스킬별로 Init 함수 호출로 초기화
                shotgun.Init(data, 1);
                // 사용하고 있는 딕셔너리에 추가
                activeWeapons.Add(data.skillName, shotgun);
                break;
            case SkillType.Orbital:
                OrbitalWeapon orbit = skillPrefab.GetComponent<OrbitalWeapon>();
                orbit.Init(data, 1);
                activeWeapons.Add(data.skillName, orbit);
                break;
            case SkillType.Projectile:
                break;
        }

    }
}
