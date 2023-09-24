using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLevel", menuName = "NewUtility/EnemyLevel")]
public class EnemyLevel : AgentConsiderations
{
    public float Enemylevel { get; private set; }

    public override void UpdateConsideration()
    {
        Enemylevel = GetState();
    }

    public override float GetState()
    {
        Enemylevel = Random.Range(0, 10);

        return Enemylevel;
    }
    public override bool GetEnemyState()
    {
        return false;
    }

    public override bool GetPreyState()
    {
        return false;
    }
}
