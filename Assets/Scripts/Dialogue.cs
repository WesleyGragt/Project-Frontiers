using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Attributes;

public class Dialogue : MonoBehaviour
{
    [SerializeField] YarnProject yarnProject;
    [SerializeField] DialogueRunner runner;
    [YarnNode(nameof(yarnProject))]
    public string startNode = "Start";

    public void StartDialogue()
    {
        runner.StartDialogue(startNode);
    }
}
