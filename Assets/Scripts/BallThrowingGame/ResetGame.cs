using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GameObject showUI;
    
    private List<Vector3> targetRot = new List<Vector3>();
    private List<Vector3> targetPos = new List<Vector3>();

    private void Update()
    {
        if (PointGiver.targets.Count == 6 && targetPos.Count == 0)
        {
            TargetTransforms();
        }

        if (BallSpawner.resetGame)
        {
            Debug.Log("Resetting BallGame");

            if (showUI != null)
            {
                showUI.SetActive(true);
            }

            BallSpawner.resetGame = false;
            BallSpawner.ballSpawning = true;

            gameObject.GetComponent<PointGiver>().ResetCansList();

            for (int i = PointGiver.targets.Count - 1; i >= 0; i--)
            {
                PointGiver.targets[i].GetComponent<Rigidbody>().isKinematic = true;
                PointGiver.targets[i].transform.eulerAngles = targetRot[i];
                PointGiver.targets[i].transform.position = targetPos[i];
            }

            for (int i = PointGiver.targets.Count - 1; i >= 0; i--)
            {
                PointGiver.targets[i].GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void TargetTransforms()
    {
        foreach (var target in PointGiver.targets)
        {
            targetRot.Add(target.transform.eulerAngles);
            targetPos.Add(target.transform.position);
        }
    }
}
