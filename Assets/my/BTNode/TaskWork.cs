using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TaskWork : Action
{
	NPC_infor npc;
	public override void OnStart()
	{
		npc = GetComponent<NPC_infor>();
		npc.Money = 100;
	}

	public override TaskStatus OnUpdate()
	{
		Debug.Log("<color=#0000FF>WORKING</color>");
		return TaskStatus.Success;
	}
}