using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] int maxBalls = 5;

    [SerializeField] private Vector3 spawnAreaSize = new Vector3(5f, 5f, 5f);

    private int spawnedBalls = 0;

    private float timer = 0;
    private float timerStart = 0f;

    private void Start()
    {
        SpawnBalls();
    }

    private void Update()
    {
        if (spawnedBalls < maxBalls)
        {
            SpawnBalls();
        }
    }

    private void SpawnBalls()
    {
        if (Time.time > timerStart + timer)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );
            Instantiate(ballPrefab, transform.position + randomPosition, Quaternion.identity);
            spawnedBalls++;

            timerStart = 0.5f;
            timer = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}