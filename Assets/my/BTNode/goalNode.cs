using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.ObjectModel;


public class goalNode : Action
{
    private AgentGOAP selectedGoap;
    private List<GAction> availableActions;

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

        // Find all game objects with 'Attack' tag and add their child objects to availableActions list.
        GameObject attackTaggedObject = GameObject.FindGameObjectWithTag(tag);

        if (attackTaggedObject != null)
        {
            foreach (Transform child in attackTaggedObject.transform)
            {
                GAction actionComponent = child.GetComponent<GAction>();
                if (actionComponent != null)
                {
                    availableActions.Add(actionComponent);
                    Debug.Log(actionComponent.actionName);
                }
            }
        }
    }

    public override TaskStatus OnUpdate()
    {

        // Calculate utility for goals represented by their afterEffects
        foreach (AgentConsiderations ac in StateVariables)
        {
            ac.UpdateConsideration();
        }


        if (selectedGoap == null)
        {
            return TaskStatus.Failure;
        }

        Debug.Log($"<color=#00FF00>GOAL: {goal} </color>");

        selectedGoap.CreateSubGoal(goal);
        selectedGoap.CarryOutPlan();
        return TaskStatus.Success;
    }
}




