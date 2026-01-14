using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PointGiver : MonoBehaviour
{
    [SerializeField] List<GameObject> Cans = new List<GameObject>();
    public static List<GameObject> targets = new List<GameObject>();

    [SerializeField] private int pointsPerCan = 10;
    [SerializeField] private int totalPoints = 0;

    private void Start()
    {
        foreach (var can in Cans)
        {
            targets.Add(can);
        }
    }

    private void Update()
    {
        GivePoints(pointsPerCan);
    }

    private void GivePoints(int points)
    {
        for (int i = Cans.Count - 1; i >= 0; i--)
        {
            var target = Cans[i];
            Vector3 upwards = target.transform.up;
            if (upwards.y > -0.5f && upwards.y < 0.5f)
            {
                totalPoints += points;
                Cans.RemoveAt(i);
            }
        }
    }

    public void ResetCansList()
    {
        Cans = new List<GameObject>(targets);
    }
}
