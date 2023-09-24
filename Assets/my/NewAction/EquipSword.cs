using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSword : GAction
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

        

        SetCurrentUtilityScore(currentUtilityScore);
    }

    // 추가된 SetCurrentUtilityScore 메서드, 현재 유틸리티 점수를 설정한다.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }

}
