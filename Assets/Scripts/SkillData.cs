using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 스킬/무기의 모든 정보를 에셋 파일로 관리합니다.
///
/// 핵심 개념:
/// 1. ScriptableObject: 데이터를 에셋 파일로 저장하는 특수 클래스
///    - MonoBehaviour처럼 GameObject에 붙이는 게 아님
///    - Project 폴더에 .asset 파일로 존재
///    - 여러 오브젝트가 같은 데이터를 공유 (메모리 절약)
///
/// 2. CreateAssetMenu: Unity 에디터에서 우클릭 → Create 메뉴에 추가
///
/// 사용법:
/// 1. Project 창에서 우클릭 → Create → Skill Data → New Skill
/// 2. Inspector에서 스킬 정보를 입력
/// 3. SkillManager에서 이 에셋들을 배열로 등록
/// </summary>
[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill Data/New Skill")]
public class SkillData : ScriptableObject
{
    // ============================================
    // 기본 정보
    // ============================================

    [Header("기본 정보")]
    [Tooltip("스킬 이름")]
    public string skillName = "New Skill";

    [Tooltip("스킬 설명 (UI에 표시)")]
    [TextArea(2, 4)]
    public string description = "스킬 설명을 입력하세요.";

    [Tooltip("스킬 아이콘 (카드 UI에 표시)")]
    public Sprite icon;

    // ============================================
    // 스킬 타입
    // ============================================

    [FormerlySerializedAs("SkillType")]
    [FormerlySerializedAs("skillType")]
    [Header("스킬 타입")]
    [Tooltip("이 스킬이 사용하는 무기 타입")]
    public SkillType WeaponSkillType = SkillType.Projectile;

    // ============================================
    // 전투 수치
    // ============================================

    [Header("전투 수치")]
    [Tooltip("기본 데미지")]
    public float baseDamage = 10f;

    [Tooltip("레벨당 데미지 증가량")]
    public float damagePerLevel = 5f;

    [Tooltip("공격 간격 (초)")]
    public float cooldown = 0.5f;

    [Tooltip("투사체 유지 시간")]
    public float lifeTime = 3f;

    [Tooltip("레벨당 쿨다운 감소량")]
    public float cooldownReductionPerLevel = 0.05f;

    [Header("투사체 설정 (Projectile 타입)")]
    [Tooltip("투사체 프리팹")]
    public GameObject projectilePrefab;

    [Tooltip("투사체 속도")]
    public float projectileSpeed = 10f;

    [Tooltip("기본 투사체 수")]
    public int baseProjectileCount = 1;

    [Header("범위 설정 (Area/Orbital 타입)")]
    [Tooltip("기본 범위/반지름")]
    public float baseRange = 2f;

    [Tooltip("레벨당 범위 증가량")]
    public float rangePerLevel = 0.3f;

    [Header("풀 매니저 연동")]
    [Tooltip("PoolManager에서 사용할 프리팹 인덱스 (Bullet = 1 등)")]
    public int itemId = 1;

    [Header("최대 레벨")]
    [Tooltip("이 스킬의 최대 레벨")]
    public int maxLevel = 5;

}

/// <summary>
/// 스킬/무기 타입 열거형
/// 각 타입에 따라 다른 무기 클래스가 사용됩니다.
/// </summary>
public enum SkillType
{
    Projectile,      // 투사체 (기본 총알)
    MultiShot,       // 다중 투사체 (산탄총)
    Orbital,         // 궤도 무기 (삽)
}
