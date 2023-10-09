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
    public override float CalculateUtility()
    {
        float currentUtilityScore = 0;

        // Get global agent considerations from GWorld instance
        AgentConsiderations[] agentConsiderationsList = GWorld.Instance.AgentConsiderations;

        // Iterate over all considerations and add their utility scores
        foreach (var consideration in agentConsiderationsList)
        {
            currentUtilityScore += consideration.GetUtility();
        }

        return currentUtilityScore;
    }
}
