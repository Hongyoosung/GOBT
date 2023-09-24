using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : GAction
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

       

        
        
        currentUtilityScore += 1f;
        


        SetCurrentUtilityScore(currentUtilityScore);
    }

    // 추가된 SetCurrentUtilityScore 메서드, 현재 유틸리티 점수를 설정한다.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }
}
