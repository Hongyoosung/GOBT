using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHealth", menuName = "NewUtility/EnemyHealth")]
public class EnemyHealth : AgentConsiderations
{
    public float enemyHealth { get; private set; }

    public override void UpdateConsideration()
    {
        enemyHealth = GetState();
    }

    public override float GetState()
    {
        enemyHealth = Random.Range(0, 50);

        return enemyHealth;
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
