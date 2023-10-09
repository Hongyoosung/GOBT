using UnityEngine;

public abstract class AgentConsiderations : ScriptableObject
{
    // Method to calculate utility value based on the current state.
    public abstract float GetUtility();

    // Method to update the state based on the agent's current condition.
    public abstract void UpdateState(Agent agent);
}
