using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectType", menuName = "NewUtility/ObjectType")]
public class ObjectType : AgentConsiderations
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
