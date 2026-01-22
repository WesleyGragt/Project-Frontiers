using UnityEngine;

public class GainPoints : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DuckTarget"))
        {
            collision.gameObject.GetComponentInParent<Animator>().enabled = true;
            collision.gameObject.GetComponentInParent<Collider>().enabled = false;

            ShootingGame.shootingPoints += ShootingGame.pointsPerHit;

            Debug.Log("Duck hit! Points: " + ShootingGame.shootingPoints);
        }

        Destroy(gameObject);
    }
}
