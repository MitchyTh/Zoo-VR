using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private GameObject shelterPoint;

    [SerializeField] private float speed = 3f;

    [SerializeField] private float moveInterval = 5f;
    [SerializeField] private float moveIntervalVar = 1f;
    [SerializeField] private float wanderRadius = 10f;

    private float moveTimer = 0f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        agent.speed = speed;
    }
    public void MoveToNewPoint()
    {
        Vector3 origin = shelterPoint != null ? shelterPoint.transform.position : transform.position;

        Vector3 randomOffset = Random.insideUnitSphere * wanderRadius;
        randomOffset.y = 0f;
        Vector3 randomPoint = origin + randomOffset;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void Start()
    {
        agent.speed = speed;
    }

    void Update()
    {
        moveTimer = Mathf.MoveTowards(moveTimer, 0, Time.deltaTime);
        if (moveTimer == 0)
        {
            MoveToNewPoint();
            moveTimer = moveInterval + Random.Range(-moveIntervalVar, moveIntervalVar);
        }
    }

    private void OnDrawGizmos()
    {
        if (shelterPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(shelterPoint.transform.position, wanderRadius);
        }
    }
}
