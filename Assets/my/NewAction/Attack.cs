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

    public override float CalculateUtility(AgentConsiderations[] agentConsiderationsList)
    {
        float currentUtilityScore = 0;

        if (agentConsiderationsList.Length < 3)
        {
            Debug.LogError("Attack ?????????? 3???? ?????????? ??????????.");
        }

        currentUtilityScore += 1f;
        
        SetCurrentUtilityScore(currentUtilityScore);
        return currentUtilityScore;
    }

    // ?????? SetCurrentUtilityScore ??????, ???? ???????? ?????? ????????.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }
}
