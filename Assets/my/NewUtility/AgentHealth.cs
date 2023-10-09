using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentHealth", menuName = "NewUtility/AgentHealth")]
public class AgentHealth : AgentConsiderations
{
    public float Health { get; private set; }

    // Implement the UpdateState method.
    public override void UpdateState(Agent agent)
    {
        // Assume the 'agent' has a 'Health' property.
        Health = agent.Health;
    }

    // Implement the CalculateUtility method.
    public override float GetUtility()
    {
        // This is a simple linear function where utility score increases with health.
        // You may need to adjust the multiplier depending on the range of health and desired utility score.
        return Health * 0.01f;
    }
}
