using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class TaskGetOffWork : Action
{
	NPC_infor npc;
	public override void OnStart()
	{
		npc = GetComponent<NPC_infor>();
		npc.isTired = true;
		npc.Money = npc.Money - 20;
		npc.isHungry = true;

	}

	public override TaskStatus OnUpdate()
	{
		Debug.Log("<color=#0000FF>GETTING OFF WORK</color>");
		return TaskStatus.Success;
	}
}