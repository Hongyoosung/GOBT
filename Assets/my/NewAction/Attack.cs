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
            Debug.LogError("Attack Ŭ�������� 3���� ��������� �ʿ��մϴ�.");
            return;
        }

       

        
        
        currentUtilityScore += 1f;
        


        SetCurrentUtilityScore(currentUtilityScore);
    }

    // �߰��� SetCurrentUtilityScore �޼���, ���� ��ƿ��Ƽ ������ �����Ѵ�.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }
}
