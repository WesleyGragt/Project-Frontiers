using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] int maxBalls = 5;

    private int spawnedBalls = 0;

    private float timer = 0;
    private float timerStart = 0f;

    private bool gameStarted = false;

    public void StartGame()
    {
        gameStarted = true;
    }

    private void Update()
    {
        if (!gameStarted) return;

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
                Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2),
                Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2),
                Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2)
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

        // Convert the local coordinate values into world
        // coordinates for the matrix transformation.
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}