using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Patrol : Action
{
    // ai�� �ʵ带 �����ϰ� �̵���
    public GameObject ai;
    public float speed = 5.0f;
    public float changeTargetTime = 2.0f;
    public float detectionRange = 10.0f; // �߰�: Ž�� ����

    private Vector3 targetPosition;
    private float elapsedTime;

    public override void OnStart()
    {
        elapsedTime = changeTargetTime; // ù ��° ��ǥ ��ġ�� �����ϵ��� elapsedTime �ʱ�ȭ
    }

    public override TaskStatus OnUpdate()
    {
        // �� Ž�� Ȯ��
        if (IsEnemyNearby())
        {
            return TaskStatus.Success; // ��� ������Ʈ�� �߰��� ��� ������ ��ȯ�ϰ� ���� ��� ����
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= changeTargetTime)
        {
            targetPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            elapsedTime = 0;
        }

        ai.transform.position = Vector3.MoveTowards(ai.transform.position, targetPosition, speed * Time.deltaTime);

        return TaskStatus.Running;
    }

    private bool IsEnemyNearby()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("enemy") || hitCollider.CompareTag("prey"))
            {
                return true;
            }
        }
        return false;
    }
}
