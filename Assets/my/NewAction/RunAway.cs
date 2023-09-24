using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }

    public override void CalculateUtility(AgentConsiderations[] agentConsiderationsList)
    {
        float currentUtilityScore = 0;

        if (agentConsiderationsList.Length < 3)
        {
            Debug.LogError("Attack 클래스에서 3개의 고려사항이 필요합니다.");
            return;
        }

        bool isEnemy = agentConsiderationsList[4].GetEnemyState();


        if(isEnemy)
        {
            // 체력이 50 미만일 경우 점수 부여 (고려사항 클래스는 agentConsiderationsList[0])
            float healthScore = agentConsiderationsList[0].GetState();
            if (healthScore < 50)
            {
                currentUtilityScore += 1f;
            }

            // 자신의 레벨이 적의 레벨보다 낮을 경우 점수 부여 (고려사항 클래스는 agentConsiderationsList[1]와 agentConsiderationsList[2])
            float levelScore = agentConsiderationsList[1].GetState() - agentConsiderationsList[2].GetState();
            if (levelScore < 0)
            {
                currentUtilityScore += 1f;
            }
        }
        

        SetCurrentUtilityScore(currentUtilityScore);
    }

    // 추가된 SetCurrentUtilityScore 메서드, 현재 유틸리티 점수를 설정한다.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }
}
