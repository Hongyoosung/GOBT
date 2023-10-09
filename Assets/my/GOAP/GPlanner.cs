using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Drawing;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<string, int> allstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;
    }
}

public class GPlanner
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states)
    {
        List<GAction> usableActions = new List<GAction>();


        foreach (GAction a in actions)
        {

            if (a.IsAchievable())
                usableActions.Add(a);

        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);


        if (!success)
        {

            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                //Debug.Log(leaf.cost);

                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
        }

        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction a in result)
            queue.Enqueue(a);

        Debug.Log("The plan is: ");
        foreach (GAction a in queue)
            Debug.Log($"<color=#00FF00> {a.actionName}</color>");

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;

        // Initialize the best action and its utility score
        GAction bestAction = null;
        float bestUtilityScore = float.NegativeInfinity;

        foreach (GAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                // Calculate the utility score of this action
                float utilityScore = action.CalculateUtility();

                // If this action's utility score is better than the current best one,
                // update the best action and its utility score.
                if (utilityScore > bestUtilityScore)
                {
                    bestAction = action;
                    bestUtilityScore = utilityScore;

                    Debug.Log($"<color=white>Action: {bestAction.actionName}, " +
                        $"Utility Score: {bestUtilityScore}</color>");
                    
                }
            }
        }

        // If no achievable actions were found, return false.
        if (bestAction == null)
            return false;

        // Apply the effects of the chosen action to get the new state.
        Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);

        foreach (KeyValuePair<string, int> eff in bestAction.effects)
        {
            if (!currentState.ContainsKey(eff.Key))
                currentState.Add(eff.Key, eff.Value);
            else
                currentState[eff.Key] += eff.Value;  // Update value for existing keys.
        }

        Node node = new Node(parent, parent.cost + bestAction.cost,
                             currentState, bestAction);

        if (GoalAchieved(goal, currentState))
        {
            leaves.Add(node);
            foundPath = true;
        }
        else
        {
            List<GAction> subset = ActionSubset(usableActions, bestAction);
            bool found = BuildGraph(node, leaves, subset, goal);

            if (found) foundPath = true;
        }

        return foundPath;
    }


    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }

        return true;
    }

    private List<GAction> ActionSubset(List<GAction> usableActions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>(usableActions);
        subset.Remove(removeMe);

        return subset;
    }
}
