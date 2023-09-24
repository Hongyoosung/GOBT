using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public string targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;

    public bool running = false;

    public goalNode goalnode;
    public AgentConsiderations[] agentConsiderations;
    public float CurrentUtilityScore; // 추가된 변수

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    // 복사 생성자
    public GAction(GAction other)
    {
        actionName = other.actionName;
        cost = other.cost;
        target = other.target;
        targetTag = other.targetTag;
        duration = other.duration;
        preConditions = other.preConditions;
        afterEffects = other.afterEffects;
        agent = other.agent;
        preconditions = other.preconditions;
        effects = other.effects;
        agentBeliefs = other.agentBeliefs;
        running = other.running;
        goalnode = other.goalnode;
        agentConsiderations = other.agentConsiderations;
        CurrentUtilityScore = other.CurrentUtilityScore; // 추가된 변수
    }

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (preConditions != null)
        {
            foreach (WorldState w in preConditions)
                preconditions.Add(w.key, w.value);
        }

        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
                effects.Add(w.key, w.value);
        }
    }

    public void InitializeAgentConsiderations(AgentConsiderations[] npcType)
    {
        agentConsiderations = npcType;
    }

    private void Reset()
    {
        actionName = GetType().ToString();
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
                return false;
        }
        return true;
    }


    // 추가된 CalculateUtility 메서드, 고려사항을 통해 각 액션 별 유틸리티 점수를 계산한다.
    public abstract void CalculateUtility(AgentConsiderations[] npc_type);
    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
