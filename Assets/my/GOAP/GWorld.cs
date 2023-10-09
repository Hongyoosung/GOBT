using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWorld : MonoBehaviour
{
    // Singleton instance.
    public static GWorld Instance { get; private set; }

    // The state of the world.
    private WorldStates world;

    // This array will be visible in the Unity editor.
    [SerializeField]
    public AgentConsiderations[] AgentConsiderations;

    void Awake()
    {
        // Set the singleton instance.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        world = new WorldStates();
    }

    public void UpdateAgentState(Agent agent)
    {
        // Update each state variable based on the current state of the agent.
        foreach (var consideration in AgentConsiderations)
        {
            consideration.UpdateState(agent);
        }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}
