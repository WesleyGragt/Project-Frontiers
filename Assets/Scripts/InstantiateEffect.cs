using Unity.VisualScripting;
using UnityEngine;

public class InstantiateEffect : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;
    [Space]
    [SerializeField] bool onEnable = true;
    [SerializeField] bool onDisable = false;
    [SerializeField] bool onCollision = false;
    [SerializeField] float scaleMultiplier = 1.0f;

    private void OnEnable()
    {
        if (!onEnable) return;

        startEffect();
    }

    private void OnDisable()
    {
        if (!onDisable) return;

        startEffect();
    }

    public void startEffect()
    {
        GameObject instance = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        instance.transform.localScale *= scaleMultiplier;

        Debug.Log("Started effect");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!onCollision) return;

        if (GetComponent<PickUp>().Thrown == false) return;

        startEffect();
    }
}
