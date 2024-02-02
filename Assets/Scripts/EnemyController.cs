using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Detector detector;

    public Action OnBuildCollected;
    private void Start()
    {
        detector.OnBuildDetected += BuildTouched;
    }

    private void OnDestroy()
    {
        detector.OnBuildDetected -= BuildTouched;
    }

    public void SetAgentDestination(Vector3 position)
    {
        var correctYPosition = new Vector3(position.x, transform.position.y, position.z);
        if (!agent.hasPath)
            agent.SetDestination(correctYPosition);
    }
    
    private void BuildTouched(Collider build)
    {
        Destroy(build.gameObject);
        OnBuildCollected?.Invoke();
    }
}
