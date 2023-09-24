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

        // ������ �Ÿ��� 25 �̻��� ��� ������ �ο�
        float distanceScore = agentConsiderationsList[3].GetState();
        if (distanceScore >= 25)
        {
            currentUtilityScore += 1f;
        }

        SetCurrentUtilityScore(currentUtilityScore);
    }

    // �߰��� SetCurrentUtilityScore �޼���, ���� ��ƿ��Ƽ ������ �����Ѵ�.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }
}
