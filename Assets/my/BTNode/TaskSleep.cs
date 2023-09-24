using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TaskSleep : Action
{

	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		
        Debug.Log("<color=#0000FF>SLEEPING</color>");

        return TaskStatus.Success;
	}
}