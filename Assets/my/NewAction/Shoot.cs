using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : GAction
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

        // 적과의 거리가 25 이상일 경우 점수를 부여
        float distanceScore = agentConsiderationsList[3].GetState();
        if (distanceScore >= 25)
        {
            currentUtilityScore += 1f;
        }

        SetCurrentUtilityScore(currentUtilityScore);
    }

    // 추가된 SetCurrentUtilityScore 메서드, 현재 유틸리티 점수를 설정한다.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }
}
