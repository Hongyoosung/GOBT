using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class goalNode : Action
{
    private AgentGOAP selectedGoap;
    private List<GAction> availableActions = new List<GAction>();

    public string goal;
    public string tag;

    public AgentConsiderations[] StateVariables;

    public override void OnStart()
    {
        // Set the AgentConsiderations array in the GWorld instance
        GWorld.Instance.AgentConsiderations = StateVariables;

        // Get the first AgentGOAP component on the GameObject
        selectedGoap = this.gameObject.GetComponent<AgentGOAP>();

        if (selectedGoap == null)
        {
            Debug.LogError("No AgentGOAP components found!");
            return;
        }

        // Find all game objects with 'tag' and add their GAction components to availableActions list.
        GameObject[] parentObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject parentObject in parentObjects)
        {
            GAction[] actions = parentObject.GetComponentsInChildren<GAction>();
            availableActions.AddRange(actions);
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (selectedGoap == null)
        {
            return TaskStatus.Failure;
        }

        // Calculate utility for goals represented by their afterEffects
        foreach (AgentConsiderations ac in StateVariables)
        {
            ac.UpdateConsideration();
        }

        Debug.Log($"<color=#00FF00>GOAL: {goal} </color>");

        selectedGoap.CreateSubGoal(goal);

        selectedGoap.CarryOutPlan(availableActions);



        return TaskStatus.Success;
    }
}
