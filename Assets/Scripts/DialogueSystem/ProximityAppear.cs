using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProximityAppear : MonoBehaviour
{
    [SerializeField] List<GameObject> npcs = new List<GameObject>();
    [SerializeField] GameObject player;

    private void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        foreach (var npc in npcs)
        {
            Vector3 pos = npc.transform.position;
            Vector3 playerPos = player.transform.position;

            float xDif = math.abs(pos.x - playerPos.x);
            float zDif = math.abs(pos.z - playerPos.z);

            float distance = Mathf.Sqrt((xDif * xDif) + (zDif * zDif));
            
            switch(distance)
            {
                case < 5:
                    npc.SetActive(true);
                    break;
                case > 5:
                    npc.SetActive(false);
                    break;
            }
        }
    }
}
