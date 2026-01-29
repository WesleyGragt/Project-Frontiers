using UnityEngine;

public class GainPoints : MonoBehaviour
{
    public GameObject hitSound;
    public GameObject pointSound;
    private void Start()
    {
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitSound, transform.position, Quaternion.identity);

        if (collision.gameObject.CompareTag("DuckTarget"))
        {
            collision.gameObject.GetComponentInParent<Animator>().enabled = true;
            collision.gameObject.GetComponentInParent<Collider>().enabled = false;

            ShootingGame.shootingPoints += ShootingGame.pointsPerHit;

            Instantiate(pointSound, transform.position, Quaternion.identity);

            Debug.Log("Duck hit! Points: " + ShootingGame.shootingPoints);
        }

        gameObject.GetComponent<InstantiateEffect>().enabled = true;
        Destroy(gameObject);
    }
}
