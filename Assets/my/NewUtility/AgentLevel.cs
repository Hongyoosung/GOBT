using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentLevel", menuName = "NewUtility/AgentLevel")]
public class AgentLevel : AgentConsiderations
{
    public float Level { get; private set; }

    public override void UpdateConsideration()
    {
        Level = GetState();
    }

    public override float GetState()
    {

        Level = Random.Range(0, 10);
        //Level = 1f;
        return Level;
    }

    public override bool GetEnemyState()
    {
        return false;
    }

    public override bool GetPreyState()
    {
        return false;
    }
}
