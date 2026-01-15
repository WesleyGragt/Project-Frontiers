using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DuckLine : MonoBehaviour
{
    [SerializeField] GameObject duckPrefab;
    [SerializeField] GameObject duckStorage;

    [SerializeField] Direction direction = Direction.ToTheRight;
    [SerializeField] float spacing = 5f;
    [SerializeField] float speed = 1f;

    private List<GameObject> ducks = new List<GameObject>();

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private void Start()
    {
        GetTransforms();
    }

    private void Update()
    {
        if (ducks.Count == 0 || CheckDistance())
        {
            SpawnDuck();
        }

        if (ducks.Count == 0) return;
        DestroyDucks();
    }

    private void GetTransforms()
    {
        spawnPosition = transform.position;

        if (direction == Direction.ToTheLeft)
        {
            spawnPosition += transform.right * transform.lossyScale.x;
        }

        Vector3 lineRotation = duckStorage.transform.eulerAngles;
        lineRotation.x = duckPrefab.transform.eulerAngles.x;
        spawnRotation = Quaternion.Euler(lineRotation);
    }

    private bool CheckDistance()
    {
        if (ducks.Count == 0) return false;

        float lastDuckPosX = ducks[ducks.Count - 1].transform.position.x;

        if (direction == Direction.ToTheRight && lastDuckPosX > spawnPosition.x + spacing)
        {
            return true;
        }
        else if (direction == Direction.ToTheLeft && lastDuckPosX < spawnPosition.x - spacing)
        {
            return true;
        }

        return false;
    }

    private void SpawnDuck()
    {
        GameObject instance = Instantiate(duckPrefab, spawnPosition, spawnRotation, duckStorage.transform);
        instance.GetComponent<DuckMovement>().SetMovement(direction, speed);
        ducks.Add(instance);
    }

    private void DestroyDucks()
    {
        float firstDuckX = ducks[0].transform.localPosition.x;

        if (direction == Direction.ToTheRight && firstDuckX > transform.lossyScale.x)
        {
            Destroy(ducks[0]);
            ducks.RemoveAt(0);
        }

        if (direction == Direction.ToTheLeft && firstDuckX < transform.localPosition.x)
        {
            Destroy(ducks[0]);
            ducks.RemoveAt(0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawLine(Vector3.zero, Vector3.right);
    }
}

public enum Direction
{
    ToTheRight,
    ToTheLeft
}