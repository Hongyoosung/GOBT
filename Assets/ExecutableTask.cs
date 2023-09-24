using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutableTask : GAction
{
    public override bool PostPerform()
    {
        return true;
    }

    public override bool PrePerform()
    {
        return true;
    }
    public override void CalculateUtility(AgentConsiderations[] agentConsiderationsList)
    {

    }

}
