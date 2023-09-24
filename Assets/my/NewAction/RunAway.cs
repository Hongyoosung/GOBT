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
            Debug.LogError("Attack Ŭ�������� 3���� ��������� �ʿ��մϴ�.");
            return;
        }

        bool isEnemy = agentConsiderationsList[4].GetEnemyState();


        if(isEnemy)
        {
            // ü���� 50 �̸��� ��� ���� �ο� (������� Ŭ������ agentConsiderationsList[0])
            float healthScore = agentConsiderationsList[0].GetState();
            if (healthScore < 50)
            {
                currentUtilityScore += 1f;
            }

            // �ڽ��� ������ ���� �������� ���� ��� ���� �ο� (������� Ŭ������ agentConsiderationsList[1]�� agentConsiderationsList[2])
            float levelScore = agentConsiderationsList[1].GetState() - agentConsiderationsList[2].GetState();
            if (levelScore < 0)
            {
                currentUtilityScore += 1f;
            }
        }
        

        SetCurrentUtilityScore(currentUtilityScore);
    }

    // �߰��� SetCurrentUtilityScore �޼���, ���� ��ƿ��Ƽ ������ �����Ѵ�.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }
}
