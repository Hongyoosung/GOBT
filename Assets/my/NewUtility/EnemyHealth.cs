using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHealth", menuName = "NewUtility/EnemyHealth")]
public class EnemyHealth : AgentConsiderations
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
