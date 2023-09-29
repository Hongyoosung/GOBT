using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics.Tracing;



public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();


    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    // Agent stats
    AgentConsiderations[] agentConsiderations;

    protected virtual void Start()
    {
        
        
    }

    bool invoked = false;
    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    

    private void LateUpdate()
    {
        // Todo : 
        //CarryOutPlan();
    }

    public void CarryOutPlan(List<GAction> actions)
    {
        if (currentAction != null && currentAction.running)
        {
            float distanceToTarget = Vector3.Distance(currentAction.target.transform.position, transform.position);
            if (currentAction.agent.hasPath && distanceToTarget < 2f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }

            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.Plan(actions, sg.Key.sgoals, null);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                    currentAction.target = GameObject.FindGameObjectWithTag(currentAction.targetTag);

                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;
            }
        }
        
    }
}
