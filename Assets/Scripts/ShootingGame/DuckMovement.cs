using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    private float speed;
    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void SetMovement(Direction direction, float movementSpeed)
    {
        if (direction == Direction.ToTheLeft)
        {

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        
        speed = movementSpeed * (direction == Direction.ToTheRight ? 1 : -1);
    }
}
