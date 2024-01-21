using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BuildsController : MonoBehaviour
{
    public GameObject[] buildPrefabs;
    public Collider spawnBoundaryCollider;
    public int spawnHeight;

    private List<GameObject> spawnedBuilds = new();
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int randomIndex = Random.Range(0, buildPrefabs.Length);
            Vector3 randomPosition = GetRandomPositionInBounds();

            GameObject build = Instantiate(buildPrefabs[randomIndex], randomPosition, Quaternion.identity);

            spawnedBuilds.Add(build);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            spawnedBuilds.ForEach(Destroy);
        }
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
