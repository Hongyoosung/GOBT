using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;
    public AgentConsiderations[] Utility;

    public Node(Node parent, float cost, Dictionary<string, int> allstates, GAction action, 
        AgentConsiderations[] utility = null)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;
        this.Utility = utility;
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
             {
                 usableActions.Add(a);
             }

         }


         List<Node> leaves = new List<Node>();
         Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), null, GWorld.Instance.AgentConsiderations);

         bool success = BuildGraph(start, leaves, usableActions, goal);

         if (!success)
         {
             Debug.Log("NO PLAN");
             return null;
         }
         else
         {
             //Debug.Log("Found a plan");
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
             Debug.Log($"<color=#00FF00>Q: {a.actionName}</color>");

         

         return queue;
     }


    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;

        // Get the best actions based on utility scores.
        List<GAction> bestActions = GetBestUtilityActions(usableActions, parent);

        foreach (GAction action in bestActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                // Apply the action's effect to the current state.
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string, int> eff in action.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                        currentState.Add(eff.Key, eff.Value);
                }

                Node node = new Node(parent,
                                     parent.cost + action.cost,
                                     currentState,
                                     action);

                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    // If the goal is not achieved yet then recursively build graph for updated goal and remaining actions.
                    Dictionary<string, int> updatedGoal = GetUpdatedGoal(goal, currentState);
                    List<GAction> remainingUsableActions = usableActions.Where(a => a != action).ToList();

                    bool found = BuildGraph(node,
                                            leaves,
                                            remainingUsableActions,
                                            updatedGoal);

                    if (found)
                        foundPath = true;
                }
            }
        }

        return foundPath;
    }




    private float CalculateTotalUtilityScore(Node node)
    {
        float totalUtilityScore = 0.0f;
        Node currentNode = node;

        while (currentNode != null)
        {
            if (currentNode.action != null)
            {
                totalUtilityScore += currentNode.action.CurrentUtilityScore;
            }
            currentNode = currentNode.parent;
        }
        return totalUtilityScore;
    }

    
    private Dictionary<string, int> GetUpdatedGoal(Dictionary<string, int> currentGoal, Dictionary<string, int> actionEffects)
    {
        Dictionary<string, int> updatedGoal = new Dictionary<string, int>(currentGoal);
        foreach (KeyValuePair<string, int> effect in actionEffects)
        {
            if (updatedGoal.ContainsKey(effect.Key))
            {
                updatedGoal[effect.Key] -= effect.Value;

                // ?????? ?? ???? goal?? ????????.
                if (updatedGoal[effect.Key] <= 0)
                {
                    updatedGoal.Remove(effect.Key);
                }
            }
        }
        return updatedGoal;
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

    private List<GAction> GetBestUtilityActions(List<GAction> usableActions, Node parent)
    {
        // Filter out actions not related to the current goal
        //usableActions = usableActions.Where(action => IsRelatedToCurrentGoal(action, goal)).ToList();

        if (usableActions.Count == 1)
        {
            //Debug.Log("Only one action");
            return new List<GAction> { usableActions[0] };
        }

        List<GAction> highestUtilityActions = new List<GAction>();
        float highestUtilityScore = float.MinValue;

        foreach (GAction action in usableActions)
        {
            float actionScore = action.CalculateUtility(parent.Utility);
            

            if (actionScore > highestUtilityScore)
            {
                highestUtilityActions.Clear();
                highestUtilityActions.Add(action);
                highestUtilityScore = actionScore;
            }
            else if (actionScore == highestUtilityScore)
            {
                highestUtilityActions.Add(action);
            }
        }

        int randomIndex = Random.Range(0, highestUtilityActions.Count);

        return new List<GAction> { highestUtilityActions[randomIndex] };
    }

    private List<GAction> ActionSubset(List<GAction> actions, Dictionary<string, int> goal)
    {
        List<GAction> subset = new List<GAction>();

        foreach (GAction action in actions)
        {
            if (action.IsAchievable() && IsEffective(action, goal))
            {
                subset.Add(action);
            }
        }

        return subset;
    }

    private bool IsEffective(GAction action, Dictionary<string, int> goal)
    {
        foreach (KeyValuePair<string, int> kvp in action.effects)
        {
            if (goal.ContainsKey(kvp.Key))
            {
                return true;
            }
        }

        return false;
    }



    private bool IsRelatedToCurrentGoal(GAction action, Dictionary<string, int> currentGoal)
    {
        foreach (var sg in currentGoal)
        {
            if (action.effects.ContainsKey(sg.Key))
                return true;
        }

        return false;
    }

    
}

