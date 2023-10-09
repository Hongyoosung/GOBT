using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentLevel", menuName = "NewUtility/AgentLevel")]
public class AgentLevel : AgentConsiderations
{
    

    // Implement the UpdateState method.
    public override void UpdateState(Agent agent)
    {
        
    }

    // Implement the CalculateUtility method.
    public override float GetUtility()
    {
        return 0;
    }
}
