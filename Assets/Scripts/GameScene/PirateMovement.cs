using UnityEngine;

public class PirateMovement : MonoBehaviour
{
    public float speed = 1f;

    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
    }
}
