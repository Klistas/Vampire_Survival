using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardUI : MonoBehaviour
{
    [Header("스킬 데이터")]
    public SkillData[] SkillDatas;

    [Header("스킬 카드")]
    public SkillCard[] SkillCards;


    /// <summary>
    /// 가지고 있는 스킬 풀에서 랜덤하게 3개뽑아서 카드에 넣어줌
    /// </summary>
    public void ShowRandomCards()
    {
        // 배열의 인덱스가 될 임의의 숫자를 가진 리스트를 만들어 준다.
        List<int> randomCards = new List<int>();

        for (int i = 0; i < SkillDatas.Length; i++)
        {
            randomCards.Add(i);
        }

        for(int i = 0; i < randomCards.Count; i++)
        {
            // 임의의 랜덤값 생성(리스트의 길이만큼 0,1,2)
            int rand = Random.Range(0, randomCards.Count);
            // 지금 돌고 있는 인덱스를 넣어줌
            int temp = randomCards[i];
            // 지금 인덱스에 랜덤한 인덱스를 넣어준다.
            randomCards[i] = randomCards[rand];
            // 랜덤한 인덱스는 현재값을 넣어준다
            randomCards[rand] = temp;
            // 결과 = 서로 바꿔준다 Fisher-Yates Shuffle
        }

        for (int i = 0; i < SkillCards.Length; i++)
        {
            // 위에서 셔플한 리스트의 크기보다 작다면
            if(i < randomCards.Count)
            {
                // 사용할 스킬의 인덱스
                int skillIndex = randomCards[i];
                // 해당 스킬을 가져옴
                SkillData skillData = SkillDatas[skillIndex];

                SkillCards[i].Init(skillData);
            }
        }
    }
}
