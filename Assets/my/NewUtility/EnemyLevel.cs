using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLevel", menuName = "NewUtility/EnemyLevel")]
public class EnemyLevel : AgentConsiderations
{
    public override void UpdateState(Agent agent)
    {

    }

    // Implement the CalculateUtility method.
    public override float GetUtility()
    {
        return 0;
    }
}
