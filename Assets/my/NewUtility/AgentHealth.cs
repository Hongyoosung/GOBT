using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentHealth", menuName = "NewUtility/AgentHealth")]
public class AgentHealth : AgentConsiderations
{
    public float Health { get; private set; }

    public override void UpdateConsideration()
    {
        Health = GetState();
    }

    public override float GetState()
    {
        Health = Random.Range(0, 100);
        //Health = 10f;
        return Health;
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
