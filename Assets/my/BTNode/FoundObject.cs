using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FoundObject : Conditional
{
    public GameObject ai;
    public float detectionRange = 10.0f; // Ž�� �Ÿ�

    public SharedBool isEnemy;
    public SharedBool isPrey;

    public override TaskStatus OnUpdate()
    {

        // ���¿� ���� ���� ������ ���� �����մϴ�.
        isEnemy.SetValue(CheckObjectsNearbyWithTag("enemy"));
        isPrey.SetValue(CheckObjectsNearbyWithTag("prey"));

        if (isEnemy.Value || isPrey.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }

    private bool CheckObjectsNearbyWithTag(string objectTag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(ai.transform.position, detectionRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(objectTag))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetIsEnemyValue()
    {
        return isEnemy.Value;
    }

    public bool GetIsPreyValue()
    {
        return isPrey.Value;
    }
}
