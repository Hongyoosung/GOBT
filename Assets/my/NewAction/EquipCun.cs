using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCun : GAction
{

    public override bool PrePerform()
    {
        Debug.Log("Equip Gun");

        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
    public override float CalculateUtility(AgentConsiderations[] agentConsiderationsList)
    {
        float currentUtilityScore = 0;

        

        SetCurrentUtilityScore(currentUtilityScore);
        return currentUtilityScore;
    }

    // ?????? SetCurrentUtilityScore ??????, ???? ???????? ?????? ????????.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }
}
