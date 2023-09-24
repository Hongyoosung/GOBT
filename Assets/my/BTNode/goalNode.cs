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
    private string goal;
    private AgentGOAP selectedGoap;

    public GAction[] Goals;
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
        foreach (GAction executableTask in Goals)
        {
            executableTask.CalculateUtility(StateVariables);
        }

        // Sort goals by descending order based on utility score
        var sortedGoals = Goals.OrderBy(item => item.CurrentUtilityScore).Reverse().ToList();

        List<GAction> selectedActions = new List<GAction> { sortedGoals[0] };
        for (int i = 1; i < sortedGoals.Count; i++)
        {
            if (Mathf.Approximately(sortedGoals[i].CurrentUtilityScore, sortedGoals[0].CurrentUtilityScore))
            {
                selectedActions.Add(sortedGoals[i]);
            }
            else
            {
                break;
            }
        }

        GAction bestAction = selectedActions[Random.Range(0, selectedActions.Count)];
        goal = bestAction.afterEffects[0].key;

        Debug.Log($"Selected goal: {goal}");

        foreach (GAction anExecutableTask in sortedGoals)
        {
            Debug.Log($"<color=#FFFF00>UTILITY OF {anExecutableTask.actionName}: {anExecutableTask.CurrentUtilityScore} " +
                $"GOAL STATE: {string.Join(", ", anExecutableTask.afterEffects.Select(state => state.key))}</color>");
        }



        if (selectedGoap == null)
        {
            return TaskStatus.Failure;
        }

        for (int i = 0; i < Goals.Length; i++)
        {
            if (Goals[i].afterEffects[0].key == goal)
            {
                Debug.Log($"<color=#00FF00>GOAL: {goal} </color>");
                string g = Goals[i].preConditions[0].key;
                selectedGoap.CreateSubGoal(g);
                selectedGoap.CarryOutPlan();
                break;
            }
        }

        return TaskStatus.Success;
    }
}



