using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Patrol : Action
{
    // ai가 필드를 랜덤하게 이동함
    public GameObject ai;
    public float speed = 5.0f;
    public float changeTargetTime = 2.0f;
    public float detectionRange = 10.0f; // 추가: 탐지 범위

    private Vector3 targetPosition;
    private float elapsedTime;

    public override void OnStart()
    {
        elapsedTime = changeTargetTime; // 첫 번째 목표 위치를 설정하도록 elapsedTime 초기화
    }

    public override TaskStatus OnUpdate()
    {
        // 적 탐지 확인
        if (IsEnemyNearby())
        {
            return TaskStatus.Success; // 상대 오브젝트를 발견한 경우 성공을 반환하고 정찰 노드 종료
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
