using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : GAction
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
        float RecovercurrentUtilityScore = 0;

        SetCurrentUtilityScore(RecovercurrentUtilityScore);
    }

    // �߰��� SetCurrentUtilityScore �޼���, ���� ��ƿ��Ƽ ������ �����Ѵ�.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }

}
