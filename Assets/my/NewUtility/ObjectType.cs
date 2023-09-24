using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectType", menuName = "NewUtility/ObjectType")]
public class ObjectType : AgentConsiderations
{
    public bool IsEnemy { get; private set; }
    public bool IsPrey { get; private set; }

    public override void UpdateConsideration()
    {
        SharedBool isEnemy = GlobalVariables.Instance.GetVariable("isEnemy") as SharedBool;
        SharedBool isPrey = GlobalVariables.Instance.GetVariable("isPrey") as SharedBool;

        IsEnemy = isEnemy.Value;
        IsPrey = isPrey.Value;
    }

    public override bool GetEnemyState()
    {
        return IsEnemy;
    }
    public override bool GetPreyState()
    {
        return IsPrey;
    }

    public override float GetState()
    {
        return 0;
    }
}
