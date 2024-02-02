using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildsController : MonoBehaviour
{
    [SerializeField] private GameObject[] buildPrefabs;
    [SerializeField] private Collider spawnBoundaryCollider;
    [SerializeField] private int spawnHeight;
    [SerializeField] private float timeToSpawn = 2f;
    [SerializeField] private float repeatRate = 3f;

    private List<GameObject> spawnedBuilds = new();

    private static BuildsController _instance;

    public Action<Vector3> OnBuildCreated;


    private void Start()
    {
        InvokeRepeating(nameof(SpawnRandomBuild), timeToSpawn,repeatRate);
    }
    
    private void SpawnRandomBuild()
    {
        int randomIndex = Random.Range(0, buildPrefabs.Length);
        Vector3 randomPosition = GetRandomPositionInBounds();

        GameObject build = Instantiate(buildPrefabs[randomIndex], randomPosition, Quaternion.identity);

        spawnedBuilds.Add(build);
        
        OnBuildCreated?.Invoke(build.transform.position);
    }
    private Vector3 GetRandomPositionInBounds()
    {
        Vector3 minBounds = spawnBoundaryCollider.bounds.min;
        Vector3 maxBounds = spawnBoundaryCollider.bounds.max;
            
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(randomX, spawnHeight, randomZ);
    }
}
