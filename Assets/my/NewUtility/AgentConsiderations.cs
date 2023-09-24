using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentConsiderations : ScriptableObject
{
    public abstract void UpdateConsideration();
    public abstract float GetState();
    public abstract bool GetEnemyState();

    public abstract bool GetPreyState();
}

