using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAfraid : GAction
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
        float GetAfraidcurrentUtilityScore = 0;

        SetCurrentUtilityScore(GetAfraidcurrentUtilityScore);
    }

    // �߰��� SetCurrentUtilityScore �޼���, ���� ��ƿ��Ƽ ������ �����Ѵ�.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }

}
