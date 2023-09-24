using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TaskGetUp : Action
{
	NPC_infor npc;
	int money;
	public override void OnStart()
	{
		npc = GetComponent<NPC_infor>();
		npc.isTired = false;
	}

	public override TaskStatus OnUpdate()
	{


		Debug.Log("<color=#0000FF>GETTING UP</color>");

		return TaskStatus.Success;
	}
}