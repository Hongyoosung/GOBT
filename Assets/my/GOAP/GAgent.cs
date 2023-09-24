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
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    List<GameObject> objectList = new List<GameObject>();
    List<GAction> gActionList = new List<GAction>();
    GameObject parentObject;
    Transform[] childTransforms;

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    // Agent stats
    AgentConsiderations[] agentConsiderations;

    protected virtual void Start()
    {
        parentObject = GameObject.Find("Goals and Actions");
        childTransforms = parentObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform childTransform in childTransforms)
        {
            if (childTransform != parentObject.transform)
            {
                GameObject childObject = childTransform.gameObject;
                objectList.Add(childObject);
            }
        }

        
        foreach (GameObject obj in objectList)
        {
            GAction[] actions = obj.GetComponents<GAction>();
            gActionList.AddRange(actions);
        }
        
        // Get all AgentConsiderations components in the scene.
        agentConsiderations = FindObjectsOfType<AgentConsiderations>();
            
        
        
        // Add the found actions to the agent's actions list.
        
        foreach (GAction a in gActionList)
            actions.Add(a);
        
    }

    bool invoked = false;
    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    // Update is called once per frame Agent Stats
    protected virtual void Update()
    {
        foreach (AgentConsiderations ac in agentConsiderations)
        {
            ac.UpdateConsideration();
            Debug.Log("Update");
        }
            
    }

    private void LateUpdate()
    {
        CarryOutPlan();
    }

    public void CarryOutPlan()
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
