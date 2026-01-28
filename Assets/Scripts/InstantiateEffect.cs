using Unity.VisualScripting;
using UnityEngine;

public class InstantiateEffect : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;
    [Space]
    [SerializeField] bool instantiateOnEnable = true;
    [SerializeField] bool instantiateOnDisable = false;
    [SerializeField] float scaleMultiplier = 1.0f;

    private void OnEnable()
    {
        if (!instantiateOnEnable) return;

        startEffect();
    }

    private void OnDisable()
    {
        if (!instantiateOnDisable) return;

        startEffect();
    }

    public void startEffect()
    {
        GameObject instance = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        instance.transform.localScale *= scaleMultiplier;

        Debug.Log("Started effect");
    }
}
