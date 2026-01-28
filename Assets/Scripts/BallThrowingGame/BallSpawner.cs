using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Animator borders;

    [SerializeField] private Collider buttonActive;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] int maxBalls = 5;

    public static List<GameObject> spawnedBalls = new List<GameObject>();

    private float timer = 0;
    private float timerStart = 0f;

    private bool gameStarted = false;
    
    public static bool ballSpawning = true;
    public static bool resetGame = false;

    public void StartGame()
    {
        if (gameStarted) return;

        borders.SetBool("Up", true);
        borders.SetBool("Down", false);

        gameStarted = true;
        
        Debug.Log("BallGame Started");
    }

    private void Update()
    {
        if (!gameStarted) return;

        if (ballSpawning && spawnedBalls.Count < maxBalls)
        {
            SpawnBalls();
        }
        else
        {
            ballSpawning = false;
        }

        if (ballSpawning) return;

        if (spawnedBalls.Count > 0)
        {
            for (int i = spawnedBalls.Count - 1; i >= 0; i--)
            {
                if (spawnedBalls[i] == null)
                {
                    spawnedBalls.RemoveAt(i);
                }
            }
        }
        else
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        borders.SetBool("Up", false);
        borders.SetBool("Down", true);

        resetGame = true;
        gameStarted = false;
        buttonActive.enabled = true;
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
            spawnedBalls.Add(Instantiate(ballPrefab, transform.position + randomPosition, Quaternion.identity, transform));

            timerStart = 0.5f;
            timer = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}