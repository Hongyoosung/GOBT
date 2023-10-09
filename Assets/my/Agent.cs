using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float Health;

    void Start()
    {
        // Initialize agent's state variables if necessary.
        GWorld.Instance.UpdateAgentState(this);
    }

    void Update()
    {
        // Update each state variable based on the current state of the agent.
        GWorld.Instance.UpdateAgentState(this);
    }
}
