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

    public string goal;
    public GAction goalAction;
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

        
        if (goalAction.afterEffects[0].key == goal)
        {
            Debug.Log($"<color=#00FF00>GOAL: {goal} </color>");
            string g = goalAction.afterEffects[0].key;
            selectedGoap.CreateSubGoal(g);
            selectedGoap.CarryOutPlan();
        }

        return TaskStatus.Success;
    }
}



