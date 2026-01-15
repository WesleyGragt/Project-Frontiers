using UnityEngine;

public class GainPoints : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DuckTarget"))
        {
            collision.gameObject.GetComponentInParent<Animator>().enabled = true;
            ShootingGame.shootingPoints += ShootingGame.pointsPerHit;

            Debug.Log("Duck hit! Points: " + ShootingGame.shootingPoints);
        }

        Destroy(gameObject);
    }
}
