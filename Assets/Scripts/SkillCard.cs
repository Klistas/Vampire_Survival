using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCard : MonoBehaviour
{

    [Header("UI 컴포넌트")]
    public Image Icon;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;

    public WeaponManager WeaponManager;

    private SkillData skillData; // 해당 카드가 어떤 스킬을 설명하는 카드인지 데이터
    
    /// <summary>
    /// 해당 카드의 데이터를 초기화 하는 함수
    /// </summary>
    /// <param name="data"></param>
    public void Init(SkillData data)
    {
        skillData = data;
        // UI 컴포넌트들을 초기화해준다.
        Icon.sprite = skillData.icon;
        NameText.text = skillData.name;
        DescriptionText.text = skillData.description;
    }

    /// <summary>
    /// 카드를 클릭했을때, WeaponManager의 SelectSkill을 실행하는 함수
    /// </summary>
    public void Select()
    {
        // WeaponManager의 스킬선택을 호출
        WeaponManager.SelectSkill(skillData);
        GameManager.instance.ResumeGame();
    }
}
